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

namespace ServiceCenter.Views
{
    /// <summary>
    /// Логика взаимодействия для PositionPage.xaml
    /// </summary>
    public partial class PositionPage : Page
    {
        public PositionPage()
        {
            InitializeComponent();
            dg_position.ItemsSource = DBConnection.GetContext().Position.ToList();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPositionPage((sender as Button).DataContext as Position));
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPositionPage(null));
        }

        private void btn_delet_Click(object sender, RoutedEventArgs e)
        {
            var PositionForRemoving = dg_position.SelectedItems.Cast<Position>().ToList();

            if (MessageBox.Show($"Вы уверены что хотите удалить {PositionForRemoving.Count()}", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    DBConnection.GetContext().Position.RemoveRange(PositionForRemoving);
                    DBConnection.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DBConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    dg_position.ItemsSource = DBConnection.GetContext().Position.ToList();
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
                dg_position.ItemsSource = DBConnection.GetContext().Position.ToList();

            }
        }

        private void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_position.ItemsSource = DBConnection.GetContext().Position.Where(x =>
            (x.PositionName.ToString().Contains(tb_search.Text.ToLower())) || 
            (x.Salary.ToString().Contains(tb_search.Text.ToLower()))  || 
            (x.Requirements.ToString().Contains(tb_search.Text.ToLower())) 
            || (x.Needs.ToString().Contains(tb_search.Text.ToLower()))).ToList();

            if (string.IsNullOrEmpty(tb_search.Text))
            {
                dg_position.ItemsSource = DBConnection.GetContext().Position.ToList();
            }
        }
    }
}
