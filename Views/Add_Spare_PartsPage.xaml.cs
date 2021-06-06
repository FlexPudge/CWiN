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
    /// Логика взаимодействия для Add_Spare_PartsPage.xaml
    /// </summary>
    public partial class Add_Spare_PartsPage : Page
    {
        private Spare_parts _currentSpare = new Spare_parts();
        public Add_Spare_PartsPage(Spare_parts selectedSpare)
        {
            InitializeComponent();
            if (selectedSpare != null)
                _currentSpare = selectedSpare;

            DataContext = _currentSpare;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentSpare.Name))
                errors.AppendLine("Введите название должности!!!");
            if (string.IsNullOrWhiteSpace(_currentSpare.Functions))
                errors.AppendLine("Укажите обязан.");
            if (string.IsNullOrWhiteSpace(_currentSpare.Price))
                errors.AppendLine("Требования");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentSpare.Id_spare_parts == 0)
            {
                DBConnection.GetContext().Spare_parts.Add(_currentSpare);
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
