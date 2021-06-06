using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServiceCenter.Models;
using System.IO;
using System.IO.Packaging;
using ServiceCenter.aboba;
using ServiceCenter.Views;
using PdfSharp.Drawing;
using System.Diagnostics;
using PdfSharp.Pdf;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ServiceCenter.Views
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page, INotifyPropertyChanged
    {
       

        public Orders CurrentOrders { get; set; }

       // public Types_of_faults CurrentTypes { get; set; }

        public OrdersPage()
        {
            InitializeComponent();
            this.DataContext = this;
            dg_orders.ItemsSource = DBConnection.GetContext().Orders.ToList();

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddOrdersPage(null));
        }

        private void btn_delet_Click(object sender, RoutedEventArgs e)
        {
            var EmployForRemoving = dg_orders.SelectedItems.Cast<Orders>().ToList();

            if (MessageBox.Show($"Вы уверены что хотите удалить сотрудника {EmployForRemoving.Count()}", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    DBConnection.GetContext().Orders.RemoveRange(EmployForRemoving);
                    DBConnection.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DBConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    dg_orders.ItemsSource = DBConnection.GetContext().Orders.ToList();
                }
                catch
                {
                    MessageBox.Show("Упс... Что-то пошло не так :(");
                }
            }
        }
        private void tb_edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddOrdersPage(CurrentOrders));
        }
       // private Orders _currentOrders;

        public event PropertyChangedEventHandler PropertyChanged;
        private Orders zakaz;


        public Orders Zakaz
        {
            get { return zakaz; }
            set { zakaz = value; OnPropertyChanged(); }
        }

        private void OnPropertyChanged([CallerMemberName] string s = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }
        private void btn_convert_Click(object sender, RoutedEventArgs e)
        {

            if (CurrentOrders == null)
                return;

            var mainFont = new XFont("Times New Roman", 22, XFontStyle.Bold);
            var font = new XFont("Times New Roman", 18, XFontStyle.Regular);

            var filename = $"Voucher{CurrentOrders.Id_orders}.pdf";

            const int x = 150;

            try
            {

                var document = PDF.NewBuilder()

                   

                   .SetTitle($"Заказ  №{CurrentOrders.Id_orders}")
                   .DrawString($"Заказ №{CurrentOrders.Id_orders}", mainFont, 0, 15)
                 


                   .DrawString($"Дата заказа:{CurrentOrders.Data_Zakaza.Value}",
                       font, x, 45, XStringFormats.CenterLeft)
                   .DrawString($"Дата возврата: {CurrentOrders.Data_of_vozvrata.Value}",
                       font, x, 75, XStringFormats.CenterLeft)
                   .DrawString($"Заказчик: {CurrentOrders.FIO_Customers}", font, x, 105, XStringFormats.CenterLeft)
                   .DrawString($"Серийный номер: {CurrentOrders.Serial_number}", font, x, 135, XStringFormats.CenterLeft)

                   .DrawString($"Тип Поломки : {CurrentOrders.id_Types_of_faults.Value} ", font, x, 165, XStringFormats.CenterLeft)
                    .DrawString($"Описание: {CurrentOrders.Types_of_faults.Description}" , font, x, 195, XStringFormats.CenterLeft)
                     .DrawString($"Симптомы:  {CurrentOrders.Types_of_faults.Simptoms}", font, x, 225, XStringFormats.CenterLeft)
                   .DrawString($"Магазин: {CurrentOrders.Shop.Name}", font, x, 255, XStringFormats.CenterLeft)
                   .DrawString($"Отметка о гарантии(есть/отсутствует): {CurrentOrders.Otmetka_o_garanti}", font, x, 285, XStringFormats.CenterLeft)

                   .DrawString($"Цена: {CurrentOrders.Price} руб.", font, x, 315, XStringFormats.CenterLeft)
                   .DrawString($"Сотрудник: {CurrentOrders.Employees.LastName} {CurrentOrders.Employees.Name} {CurrentOrders.Employees.MiddleName}", font, x, 345, XStringFormats.CenterLeft)

                   

                   .Build() as PdfDocument;
                   document.Save(filename);

                var process = new Process();
                process.StartInfo = new ProcessStartInfo(filename)
                {
                    UseShellExecute = true
                };
                process.Start();
            }
            catch 
            {
                //App.ShowNotification("Ошибка pdf формат", e.Message);
                MessageBox.Show("Сегодня мы не работаем");
            }


        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dg_orders.ItemsSource = DBConnection.GetContext().Orders.ToList();

            }
        }

        private void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_orders.ItemsSource = DBConnection.GetContext().Orders.Where(x =>
             (x.FIO_Customers.ToString().Contains(tb_search.Text.ToLower())) ||
             (x.Data_Zakaza.ToString().Contains(tb_search.Text.ToLower()))
                || (x.Data_of_vozvrata.ToString().Contains(tb_search.Text.ToLower())) ||
                (x.Employees.LastName.ToString().Contains(tb_search.Text.ToLower())) ||
                 (x.Employees.Name.ToString().Contains(tb_search.Text.ToLower())) ||
                   (x.Shop.Name.ToString().Contains(tb_search.Text.ToLower())) ||
                  (x.Employees.MiddleName.ToString().Contains(tb_search.Text.ToLower()))).ToList();

            if (string.IsNullOrEmpty(tb_search.Text))
            {
                dg_orders.ItemsSource = DBConnection.GetContext().Orders.ToList();
            }




//            Clients = Clients.Where(p => Services.Math.LevenshteinDistance(p.Name, NameSearch) < 6 ||
//Services.Math.LevenshteinDistance(p.Surname, NameSearch) < 6 ||
//Services.Math.LevenshteinDistance(p.Patronymic, NameSearch) < 6).ToList();

//            NameSearch = строка из TextBox

        }
    }
}
