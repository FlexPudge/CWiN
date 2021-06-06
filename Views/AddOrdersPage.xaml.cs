using ServiceCenter.Models;
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

namespace ServiceCenter.Views
{

    public partial class AddOrdersPage : Page, INotifyPropertyChanged
    {
        private Orders _currentOrders = new Orders();

        public event PropertyChangedEventHandler PropertyChanged;

        public Orders CurrentOrders
        {
            get => _currentOrders;
            set
            {
                _currentOrders = value;
                OnPropertyChanged();
            }
        }

        public AddOrdersPage(Orders selectedOrders)
        {
            InitializeComponent();
            if (selectedOrders != null)
                _currentOrders = selectedOrders;

            this.DataContext = this;


            //cb_fauls.ItemsSource = DBConnection.GetContext().Types_of_faults.ToList();


            //для cb сотрудников 

            Employees = DBConnection.GetContext().Employees.ToList();
            Employee = _currentOrders.Employees;

            Types = DBConnection.GetContext().Types_of_faults.ToList();

            Type = _currentOrders.Types_of_faults;

            Shops = DBConnection.GetContext().Shop.ToList();

            Shop = _currentOrders.Shop;



        }

        private List<Shop> shops;

        public List<Shop> Shops
        {
            get { return shops; }
            set { shops = value;
                OnPropertyChanged();
            }

        }

        private Shop shop;

        public Shop Shop
        {
            get { return shop; }
            set
            {
                shop = value;
                OnPropertyChanged();
            }
        }

        







        private List<Types_of_faults> types  ;    //positions

        public List<Types_of_faults> Types    //Positions
        {
            get { return types; }  //positions
            set
            {
                types = value;      //positions
                OnPropertyChanged();
            }
        }

        private Types_of_faults type;    //position


        public Types_of_faults Type      //Position
        {
            get { return type; }
            set { type = value; OnPropertyChanged(); }
        }






        private List<Employees> employees;    //positions

        public List<Employees> Employees    //Positions
        {
            get { return employees; }  //positions
            set
            {
                employees = value;      //positions
                OnPropertyChanged();
            }
        }

        private Employees employee;    //position


        public Employees Employee      //Position
        {
            get { return employee; }
            set { employee = value; OnPropertyChanged(); }
        }


        private void OnPropertyChanged([CallerMemberName] string s = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentOrders.Data_Zakaza.Value==null)
                errors.AppendLine("Укажите дату заказа");





            if (_currentOrders.Data_of_vozvrata.Value==null)
                errors.AppendLine("Укажите дату возврата");
            if(_currentOrders.Data_of_vozvrata.Value <= _currentOrders.Data_Zakaza.Value)
                errors.AppendLine("С датой что то не так )=");


            if (string.IsNullOrWhiteSpace(_currentOrders.FIO_Customers))
                errors.AppendLine("Введите ФИО Заказчика");
            if (string.IsNullOrWhiteSpace(_currentOrders.Serial_number))
                errors.AppendLine("Укажите серийный номер");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            

            if (_currentOrders.Id_orders == 0)
            {
                DBConnection.GetContext().Orders.Add(_currentOrders);
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
