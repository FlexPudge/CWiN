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
    /// Логика взаимодействия для AddShopPage.xaml
    /// </summary>
    public partial class AddShopPage : Page
    {
        private Shop _currentShop = new Shop();
        public AddShopPage(Shop selectedshop)
        {
            InitializeComponent();

            if (selectedshop != null)
                _currentShop = selectedshop;

            DataContext = _currentShop;

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentShop.Name))
                errors.AppendLine("Введите название ");
            if (string.IsNullOrWhiteSpace(_currentShop.Address))
                errors.AppendLine("Укажите адрес");
            if (string.IsNullOrWhiteSpace(_currentShop.Phone_number))
                errors.AppendLine("Номер телефона");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentShop.Id_shop == 0)
            {
                DBConnection.GetContext().Shop.Add(_currentShop);
            }

            try
            {
                DBConnection.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch
            {
                MessageBox.Show("Упс... что то пошло не так");

            }
        }

       
    }
    }

