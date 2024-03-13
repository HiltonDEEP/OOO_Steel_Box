using OOO_Steel_Box.Classes;
using OOO_Steel_Box.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace OOO_Steel_Box
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //подключение к БД
            Helper.DB = new SteelBoxDBEntitiy();
        }

        private void Gost_Click(object sender, RoutedEventArgs e)
        {
            // Открываем второе окно
            Catalog catalog = new Catalog();
            this.Close();
            catalog.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // закрыть текущее окно
        }


        private void isVisiblePassword_Checked(object sender, RoutedEventArgs e)
        {
            PasswordShow.Text = PasswordDot.Password; // скопируем в TextBox из PasswordBox
            PasswordShow.Visibility = Visibility.Visible; // TextBox - отобразить
            PasswordDot.Visibility = Visibility.Hidden; // PasswordBox - скрыть
        }

        private void isVisiblePassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordDot.Password = PasswordShow.Text; // скопируем в PasswordBox из TextBox 
            PasswordShow.Visibility = Visibility.Hidden; // TextBox - скрыть
            PasswordDot.Visibility = Visibility.Visible; // PasswordBox - отобразить
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //Catalog catalog = new Catalog();
            //this.Close(); // Закрываем первое окно
            //catalog.Show(); // Открываем второе окно

            string login = Login.Text;
            string passwordHide = PasswordDot.Password;
            PasswordShow.Text = PasswordDot.Password;
            string passwordShow = PasswordShow.Text;

            StringBuilder sb = new StringBuilder();
            if (login == "")
            {
                sb.Append("Вы не ввели логин.\n");
            }
            if (passwordHide == "" || passwordShow == "")
            {
                sb.Append("Вы не ввели пароль.\n");
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }

            List<Model.Users> users = new List<Model.Users>();
            users = Helper.DB.Users.ToList();
            Helper.User = users.Where(u => u.UserLogin == login && (u.UserPassword == passwordHide || u.UserPassword == passwordShow)).FirstOrDefault();

            if (Helper.User != null)
            {
                //Доступ к конкретному полю
                string userName = Helper.User.UserFullName;
                int userRoleID = Helper.User.UserRoleID;
                //Доступ по навигационному свойству к полю связанной таблицы
                string userRoleName = Helper.User.UserRoles.UserRoleName;
                MessageBox.Show(userName + "\nКод роли: " + userRoleID + "\nНазвание роли: " + userRoleName);
                Catalog catalog = new Catalog();
                catalog.Show();
                this.Close();

            }
        }
    }
}
