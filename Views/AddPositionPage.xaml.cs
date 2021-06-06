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
 
    public partial class AddPositionPage : Page
    {
        private Position _currentPosition = new Position();
        public AddPositionPage(Position selectedPosition)
        {
            InitializeComponent();
            if (selectedPosition != null)
                _currentPosition = selectedPosition;

            DataContext = _currentPosition;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPosition.PositionName))
                errors.AppendLine("Введите название должности!!!");
            if (string.IsNullOrWhiteSpace(_currentPosition.Needs))
                errors.AppendLine("Укажите обязан.");
            if (string.IsNullOrWhiteSpace(_currentPosition.Requirements))
                errors.AppendLine("Требования");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentPosition.Id_position == 0)
            {
                DBConnection.GetContext().Position.Add(_currentPosition);
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
