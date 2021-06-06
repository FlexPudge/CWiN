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
    /// Логика взаимодействия для ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        public ShopPage()
        {
            InitializeComponent();
            dg_shop.ItemsSource = DBConnection.GetContext().Shop.ToList();
        }

        private void btn_delet_Click(object sender, RoutedEventArgs e)
        {
            var PositionForRemoving = dg_shop.SelectedItems.Cast<Shop>().ToList();

            if (MessageBox.Show($"Вы уверены что хотите удалить {PositionForRemoving.Count()}", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    DBConnection.GetContext().Shop.RemoveRange(PositionForRemoving);
                    DBConnection.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DBConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    dg_shop.ItemsSource = DBConnection.GetContext().Shop.ToList();
                }
                catch
                {
                    MessageBox.Show("Упс... Что-то пошло не так :(");
                }
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddShopPage(null));
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddShopPage((sender as Button).DataContext as Shop));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dg_shop.ItemsSource = DBConnection.GetContext().Shop.ToList();

            }
        }
    }
}
