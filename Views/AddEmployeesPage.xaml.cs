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
using ServiceCenter.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;



namespace ServiceCenter.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeesPage.xaml
    /// </summary>
    public partial class AddEmployeesPage : Page, INotifyPropertyChanged
    {
        private Employees _currentEmploy = new Employees();
        public Employees CurrentEmployee
        {
            get => _currentEmploy;
            set
            {
                _currentEmploy = value;
                OnPropertyChanged();
            }
        }

        public AddEmployeesPage(Employees selectedEmploy)
        {
            InitializeComponent();
            if (selectedEmploy != null)
                _currentEmploy = selectedEmploy;

            this.DataContext = this;
            //DataContext = _currentEmploy;
            Positions = DBConnection.GetContext().Position.ToList();
            Position = _currentEmploy.Position;
            //cbPosition.ItemsSource = Service_centerEntities1.GetContext().Position.ToList();
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
        

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentEmploy.LastName))
                errors.AppendLine("Введите Фамилию");
            int n;
            if (int.TryParse(tbLastName.Text, out n))
                errors.AppendLine("Введено число!");
            if (int.TryParse(tbName.Text, out n))
                errors.AppendLine("Введено число!");
            if (int.TryParse(tbMiddleName.Text, out n))
                errors.AppendLine("Введено число!");

            if (string.IsNullOrWhiteSpace(_currentEmploy.Name))
                errors.AppendLine("Введите Имя");
            if (string.IsNullOrWhiteSpace(_currentEmploy.MiddleName))
                errors.AppendLine("Введите Отчество");
            if (string.IsNullOrWhiteSpace(_currentEmploy.Age))
                errors.AppendLine("Укажите возраст");
            if (string.IsNullOrWhiteSpace(_currentEmploy.Gender))
                errors.AppendLine("Укажите пол");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (e.Handled==false)
            {
               errors.AppendLine("Введено число!");
            
            }

            if (_currentEmploy.Id_empl == 0)
            {
                DBConnection.GetContext().Employees.Add(_currentEmploy);
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

        private List<Position> positions;

        public List<Position> Positions
        {
            get { return positions; }
            set
            {
                positions = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string s = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }
        private Position position;


        public Position Position
        {
            get { return position; }
            set { position = value; OnPropertyChanged(); }
        }

        private void tbLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (!Char.IsLetter(c))
            {
                // число
                e.Handled = false;
            }
            else
            {
                // не число
                e.Handled = false;
            }
            base.OnPreviewTextInput(e);
        }

        private void tbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (!Char.IsLetter(c))
            {
                // число
                e.Handled = false;
            }
            else
            {
                // не число
                e.Handled = false;
            }
            base.OnPreviewTextInput(e);
        }

        private void tbMiddleName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (!Char.IsLetter(c))
            {
                // число
                e.Handled = false;
            }
            else
            {
                // не число
                e.Handled = false;
            }
            base.OnPreviewTextInput(e);
        }
    }
}
