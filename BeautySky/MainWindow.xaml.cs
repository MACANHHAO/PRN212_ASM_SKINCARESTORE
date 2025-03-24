using DAL.BeautySky.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.BeautySky.Services;
using DAL.BeautySky.Repositories;
using DAL.BeautySky;

namespace BeautySky
{

    public partial class MainWindow : Window
    {
        public User AuthenticatedUser { get; set; }
        private ProductService _service = new();
        private User user = new();
        private readonly OrderService _orderService;

        public MainWindow()
        {
            InitializeComponent();

            var dbContext = new ProjectSwpContext();
            var orderRepository = new OrderRepository(dbContext);
            _orderService = new OrderService(orderRepository);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ProductListDataGrid.ItemsSource = _service.GetAllProduct();
            //2. DISABLE NÚT BẤM NẾU LÀ STAFF
            if (AuthenticatedUser != null && AuthenticatedUser.RoleId == 1)
            {
                CreateButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                BuyButton.IsEnabled = false;
                ManagerButton.IsEnabled = false;
            }
            if (AuthenticatedUser != null && AuthenticatedUser.RoleId == 2)
            {
                ManagerButton.IsEnabled = false;
                BuyButton.IsEnabled = false;
            }
            if (AuthenticatedUser != null && AuthenticatedUser.RoleId == 3)
            {
                BuyButton.IsEnabled = false;
            }




        }

        private void FillDataGrid(List<Product> arr)
        {
            ProductListDataGrid.ItemsSource = null; 
            ProductListDataGrid.ItemsSource = arr;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DetailWindow detail = new();
            detail.ShowDialog(); 
            FillDataGrid(_service.GetAllProduct());
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Product? selected = ProductListDataGrid.SelectedItem as Product;

            if (selected == null)
            {
                MessageBox.Show("Please select an air-con before deleting", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            MessageBoxResult answer = MessageBox.Show("Do you really want to delete this air-con?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
                return;
            _service.DeleteProduct(selected); 
            FillDataGrid(_service.GetAllProduct());

        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = ProductNameTextBox.Text.Trim();


            List<Product> searchResults = _service.SearchProducts(keyword);

            if (searchResults.Any())
            {
   
                ProductListDataGrid.ItemsSource = searchResults;
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void ProductListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductListDataGrid.SelectedItem is not Product selectedProduct)
                {
                    string message = ProductListDataGrid.Items.Count == 0
                        ? "No products available for purchase."
                        : "Please select a product before buying.";
                    MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int userId = AuthenticatedUser?.UserId ?? 0;
                if (userId == 0)
                {
                    MessageBox.Show("User not logged in!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Hộp thoại xác nhận mua
                MessageBoxResult confirmBuy = MessageBox.Show(
                    "Xác nhận mua món hàng này?" +
                    "Confirm purchase of this item?",
                    "Purchase Confirmation",
                    MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (confirmBuy != MessageBoxResult.OK) return;

                // Hộp thoại yêu cầu chuyển tiền
                MessageBoxResult confirmPayment = MessageBox.Show(
                    "Hãy chuyển tiền vào STK 654654613464653545 MB Bank" +
                    "Please transfer money to account number 654654613464653545 MB Bank",
                    "Payment Required",
                    MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (confirmPayment != MessageBoxResult.Yes) return;

                // Xác nhận đã chuyển tiền -> Tạo đơn hàng
                _orderService.CreateOrder(userId, selectedProduct.ProductId, selectedProduct.Price);
                MessageBox.Show("Purchase successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Cập nhật danh sách sản phẩm
                FillDataGrid(_service.GetAllProduct());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow order = new();
            order.ShowDialog();
            FillDataGrid(_service.GetAllProduct());
        }

      
    }
}