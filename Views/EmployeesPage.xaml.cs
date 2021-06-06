using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ServiceCenter.Views;
namespace ServiceCenter.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page, INotifyPropertyChanged
    {
        public EmployeesPage()
        {

            InitializeComponent();
            this.DataContext = this;
            dg_employ.ItemsSource = DBConnection.GetContext().Employees.ToList();


        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEmployeesPage(null));
        }

        private void btn_delet_Click(object sender, RoutedEventArgs e)
        {
            var EmployForRemoving = dg_employ.SelectedItems.Cast<Employees>().ToList();

            if (MessageBox.Show($"Вы уверены что хотите удалить сотрудника {EmployForRemoving.Count()}", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    DBConnection.GetContext().Employees.RemoveRange(EmployForRemoving);
                    DBConnection.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DBConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    dg_employ.ItemsSource = DBConnection.GetContext().Employees.ToList();
                }
                catch
                {
                    MessageBox.Show("Упс... Что-то пошло не так :(");
                }
            }
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEmployeesPage(Employee));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dg_employ.ItemsSource = DBConnection.GetContext().Employees.ToList();

            }
        }
        private Employees employee;


        public Employees Employee
        {
            get { return employee; }
            set { employee = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string s = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }
     // private Position position;

        private void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_employ.ItemsSource = DBConnection.GetContext().Employees.Where(x =>
             (x.LastName.ToString().Contains(tb_search.Text.ToLower())) || 
             (x.Name.ToString().Contains(tb_search.Text.ToLower())) ||
              (x.MiddleName.ToString().Contains(tb_search.Text.ToLower())) ||
               (x.Address.ToString().Contains(tb_search.Text.ToLower())) ||
                (x.Age.ToString().Contains(tb_search.Text.ToLower())) ||
              (x.Position.PositionName.ToString().Contains(tb_search.Text.ToLower()))).ToList();

            if (string.IsNullOrEmpty(tb_search.Text))
            {
                dg_employ.ItemsSource = DBConnection.GetContext().Employees.ToList();
            }
        }

        private void dg_employ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
