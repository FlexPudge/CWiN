using ServiceCenter.Models;
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
using ServiceCenter.Views;


namespace ServiceCenter.Views
{
    
    public partial class Spare_PartsPage : Page
    {
        public Spare_PartsPage()
        {
            InitializeComponent();
            dg_spareparts.ItemsSource = DBConnection.GetContext().Spare_parts.ToList();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Add_Spare_PartsPage((sender as Button).DataContext as Spare_parts));
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Add_Spare_PartsPage(null));
        }

        private void btn_delet_Click(object sender, RoutedEventArgs e)
        {
            var SPForRemoving = dg_spareparts.SelectedItems.Cast<Spare_parts>().ToList();

            if (MessageBox.Show($"Вы уверены что хотите удалить сотрудника {SPForRemoving.Count()}", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    DBConnection.GetContext().Spare_parts.RemoveRange(SPForRemoving);
                    DBConnection.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DBConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    dg_spareparts.ItemsSource = DBConnection.GetContext().Spare_parts.ToList();
                }
                catch
                {
                    MessageBox.Show("Упс... Что-то пошло не так :(");
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dg_spareparts.ItemsSource = DBConnection.GetContext().Spare_parts.ToList();

            }
        }

        private void dg_spareparts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_spareparts.ItemsSource = DBConnection.GetContext().Spare_parts.Where(x =>
           (x.Name.ToString().Contains(tb_search.Text.ToLower())) ||
           (x.Functions.ToString().Contains(tb_search.Text.ToLower())) ||
           (x.Price.ToString().Contains(tb_search.Text.ToLower()))).ToList();

            if (string.IsNullOrEmpty(tb_search.Text))
            {
                dg_spareparts.ItemsSource = DBConnection.GetContext().Spare_parts.ToList();
            }
        }
    }
}
