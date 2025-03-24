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
                Product? selectedProduct = ProductListDataGrid.SelectedItem as Product;
                if (selectedProduct == null)
                {
                    MessageBox.Show("Please select a product before buying.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int productId = selectedProduct.ProductId;
                decimal unitPrice = selectedProduct.Price;

                int userId = AuthenticatedUser?.UserId ?? 0;
                if (userId == 0)
                {
                    MessageBox.Show("User not logged in!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _orderService.CreateOrder(userId, productId, unitPrice);

                MessageBox.Show("Purchase successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}