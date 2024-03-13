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
    /// Логика взаимодействия для OrderDesc.xaml
    /// </summary>
    public partial class OrderDesc : Window
    {
        List<Model.Orders> listOrder = new List<Model.Orders>();
        List<Model.PCBuilds> listOrderInfo = new List<Model.PCBuilds>();
        List<OrderExtended> listOrderExtended;
        public OrderDesc()
        {
            InitializeComponent();
            listOrder = Helper.DB.Orders.ToList();
            listOrderInfo = Helper.DB.PCBuilds.ToList();
            listOrderExtended = new List<OrderExtended>();

            foreach (var order in listOrder)
            {
                Classes.OrderExtended orderExtended = new Classes.OrderExtended();
                orderExtended.order = order;
                // Получаем список компонентов для данного заказа
                orderExtended.pcBuilds = listOrderInfo.Where(info => info.PCBuildID == order.PCBuildID).ToList();
                listOrderExtended.Add(orderExtended);
            }
            lvOrders.ItemsSource = listOrderExtended;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            Helper.User = null;
            this.Close();
            window.Show();
        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что выбран элемент
            if (lvOrders.SelectedItem != null)
            {
                // Получаем выбранный заказ
                OrderExtended selectedOrder = lvOrders.SelectedItem as OrderExtended;

                // Получаем информацию о выбранном заказе
                List<Model.PCBuilds> selectedOrderInfo = listOrderInfo.Where(info => info.PCBuildID == selectedOrder.order.PCBuildID).ToList();

                // Очищаем lvSelectOrder
                lvSelectOrder.ItemsSource = null;

                // Заполняем lvSelectOrder информацией о выбранном заказе
                lvSelectOrder.ItemsSource = selectedOrderInfo;
            }
        }
    }
}
