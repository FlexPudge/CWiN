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
using System.Windows.Media.Animation;
using ServiceCenter.Views;


namespace ServiceCenter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame;
        }

        private void btn_empl_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EmployeesPage());
           lb.Visibility = Visibility.Hidden;
            img.Visibility = Visibility.Hidden;
        }

        private void btn_posit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PositionPage());
            lb.Visibility = Visibility.Hidden;
            img.Visibility = Visibility.Hidden;

        }

        private void btn_spare_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Spare_PartsPage());
            lb.Visibility = Visibility.Hidden;
            img.Visibility = Visibility.Hidden;
        }

        private void btn_rem_model_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_type_faul_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_ops_shop_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ShopPage());
            lb.Visibility = Visibility.Hidden;
            img.Visibility = Visibility.Hidden;
        }

        private void btn_orders_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new OrdersPage());
            lb.Visibility = Visibility.Hidden;
            img.Visibility = Visibility.Hidden;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                if (MainFrame.CanGoBack)
                {
                    btn_back.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_back.Visibility = Visibility.Hidden;
                }
            }
            catch
            {
               
            }
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
