using OOO_Steel_Box.Classes;
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

namespace OOO_Steel_Box
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        // Ссылка на главное окно

        List<PCBuildExtended> pCBuildExtendeds = new List<PCBuildExtended>();

        public Catalog()
        {
            InitializeComponent();
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Helper.User != null)
            {
                tbFIO.Text = Helper.User.UserFullName;
            }
            else
            {
                tbFIO.Text = "Гость";
            }
            comboSale.Items.Clear();
            comboSale.Items.Add("Все диапазоны");
            comboSale.Items.Add("0-9%");
            comboSale.Items.Add("10-14%");
            comboSale.Items.Add("15% и более");
            comboSale.SelectedIndex = 0;

            comboSort.Items.Clear();
            comboSort.Items.Add("Любая цена");
            comboSort.Items.Add("По возрастанию");
            comboSort.Items.Add("По убыванию");
            comboSort.SelectedIndex = 0;

            if (Helper.User == null)
            {   
                buttonOrder.Visibility = Visibility.Hidden;
                buttonEdit.Visibility = Visibility.Hidden;
            }
            else if(Helper.User.UserRoleID == 1)
            {
                buttonOrder.Visibility = Visibility.Hidden;
                buttonEdit.Visibility = Visibility.Hidden;
            }
            else
            {
                if (Helper.User.UserRoleID == 2)
                {
                    OrderDesc orders = new OrderDesc();
                    this.Close();
                    orders.Show();
                }
                else
                {
                    buttonOrder.Visibility = Visibility.Hidden;
                }          
            }
        }
        private void ShowProduct()
        {

            List<Model.PCBuilds> pcbuild = new List<Model.PCBuilds>();
            pcbuild = Helper.DB.PCBuilds.ToList();

            int totalCount = pcbuild.Count;

            double maxDiscount = 100;
            double minDiscount = 0;

            switch (comboSale.SelectedIndex)
            {
                case 1:
                    maxDiscount = 9;
                    break;
                case 2:
                    minDiscount = 10;
                    maxDiscount = 14;
                    break;
                case 3:
                    minDiscount = 15;
                    break;
            }

            //Осуществить фильтрации по выбранной скидке
            pcbuild = pcbuild.Where(pr => pr.PCBuildDiscount <= maxDiscount && pr.PCBuildDiscount >= minDiscount).ToList();

            //Сортировка от состояния радиокнопки
            if (comboSort.SelectedIndex == 1)		//По возрастанию
            {
                pcbuild = pcbuild.OrderBy(pr => pr.PCBuildPrice).ToList();
            }
            else if (comboSort.SelectedIndex == 2)
            {
                pcbuild = pcbuild.OrderByDescending(pr => pr.PCBuildPrice).ToList();
            }
            //Фильтрация по категории в случае, если не все категории
            //if (comboCategory.SelectedIndex > 0)
            //{
            //    searchName.Visibility = Visibility.Visible;
            //    products = products.Where(pr => pr.ProductCategory == comboCategory.SelectedIndex).ToList();
            //}
            //Поиск по названию
            string search = searchBox.Text;
            search = search.ToLower();
            if (!string.IsNullOrEmpty(search))
            {
                pcbuild = pcbuild.Where(pr => pr.PCBuildName.ToLower().Contains(search)).ToList();

            }

            //Отображение после выполнения всех фильтраций
            int filterCount = pcbuild.Count;
            tbCount.Text = filterCount.ToString() + " из " + totalCount.ToString();
            //listBoxProducts.ItemsSource = products;

            List<Classes.PCBuildExtended> productExtendeds = new List<Classes.PCBuildExtended>();
            foreach (Model.PCBuilds i in pcbuild)
            {
                Classes.PCBuildExtended productExtended = new Classes.PCBuildExtended();
                productExtended.PCBuilds = i;
                productExtendeds.Add(productExtended);
            }

            listBoxProducts.ItemsSource = productExtendeds;
        }


        private void comboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProduct();
        }

        private void comboSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProduct();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowProduct();
        }

        private void cmAddInOrder_Click_1(object sender, RoutedEventArgs e)
        {
            if(Helper.User.UserRoleID == 1)
            {
                buttonOrder.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Вы не имеете права оформлять заказ");
            }
        }

        private void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderClient client = new OrderClient();
            Helper.User = null;
            this.Close();
            client.Show();
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            this.Close();
            editWindow.Show();
        }


    }
}
