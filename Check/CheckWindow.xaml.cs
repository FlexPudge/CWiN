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
using System.Windows.Shapes;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ServiceCenter.Views;

namespace ServiceCenter.Check
{
    /// <summary>
    /// Логика взаимодействия для CheckWindow.xaml
    /// </summary>
    public partial class CheckWindow : Window
    {
        public string login = string.Empty;
        public string password = string.Empty;
        private Users user = new Users();
        public CheckWindow()
        {
            InitializeComponent();
            LoadUsers();
        }
        private void LoadUsers()
        {
            try
            {
                FileStream fs = new FileStream("Users.txt", FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();

                user = (Users)formatter.Deserialize(fs);

                fs.Close();
            }
            catch { return; }
        }
        private void EnterToForm()
        {
            for (int i = 0; i < user.Logins.Count; i++) 
            {
                if (user.Logins[i] == loginTextBox.Text && user.Passwords[i] == passwordTextBox.Text)
                {
                    login = user.Logins[i];
                    password = user.Passwords[i];


                    MainWindow b = new MainWindow();
                    b.Show();
                    this.Close();


                }
                else if (user.Logins[i] == loginTextBox.Text && passwordTextBox.Text != user.Passwords[i])
                {
                    login = user.Logins[i];

                    MessageBox.Show("Неверный пароль!");
                }
            }

            if (login == "") { MessageBox.Show("Пользователь " + loginTextBox.Text + " не найден!"); }
        }
        private void AddUser() 
        {
            if (loginTextBox.Text == "" || passwordTextBox.Text == "") { MessageBox.Show("Не введен логин или пароль!"); return; }

            user.Logins.Add(loginTextBox.Text);
            user.Passwords.Add(passwordTextBox.Text);

            FileStream fs = new FileStream("Users.txt", FileMode.OpenOrCreate);

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, user); 
            MessageBox.Show("Есть!");
            fs.Close();

            login = loginTextBox.Text;


        }

        private void tb_next_Click(object sender, RoutedEventArgs e)
        {
            EnterToForm();

        }

        private void tb_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_1_Click(object sender, RoutedEventArgs e)
        {
            btn_2.Visibility = Visibility.Visible;
        }

        private void btn_2_Click(object sender, RoutedEventArgs e)
        {
            btn_3.Visibility = Visibility.Visible;
        }

        private void btn_3_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
        }
    }
}
