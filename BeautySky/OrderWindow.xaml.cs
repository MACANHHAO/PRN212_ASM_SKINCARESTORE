using BLL.BeautySky.Services;
using DAL.BeautySky;
using DAL.BeautySky.Models;
using DAL.BeautySky.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BeautySky
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
 
        public ObservableCollection<Order> Orders { get; set; }
        public Order? SelectedOrder { get; set; }
        private readonly OrderService _ord;

        public OrderWindow()
        {
            InitializeComponent();

            // Tạo instance của OrderRepository trước
            var orderRepository = new OrderRepository(new ProjectSwpContext());

            // Truyền orderRepository vào OrderService
            _ord = new OrderService(orderRepository);

            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dgOrders.ItemsSource = _ord.GetAllOrder();


        }

        private void FillDataGrid(List<Product> arr)
        {
            dgOrders.ItemsSource = null;
            dgOrders.ItemsSource = arr;
        }
        // Xử lý xác nhận đơn hàng
        private void ConfirmOrder(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder == null)
            {
                MessageBox.Show("Please select an order to confirm.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedOrder.Status == "Completed")
            {
                MessageBox.Show("This order is already completed!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Xác nhận trước khi cập nhật trạng thái
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to mark Order {SelectedOrder.OrderId} as Completed?",
                                                      "Confirm Order", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    SelectedOrder.Status = "Completed";

                    // Gọi cập nhật xuống database
                    _ord.UpdateOrderStatus(SelectedOrder.OrderId, "Completed");

                    // Load lại danh sách đơn hàng từ database để cập nhật UI
                    dgOrders.ItemsSource = _ord.GetAllOrder();
                    dgOrders.Items.Refresh();

                    MessageBox.Show($"Order {SelectedOrder.OrderId} has been marked as Completed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating order: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        // Xử lý thoát cửa sổ
        private void QuitWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}