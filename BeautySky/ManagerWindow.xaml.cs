using BLL.BeautySky.Services;
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

namespace BeautySky
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {

        private UserService _userService = new(); // Khởi tạo UserService
        public ManagerWindow()
        {
            InitializeComponent();
            LoadUsers(); // Load danh sách user khi mở cửa sổ
        }
        private void LoadUsers()
        {
            UserDataGrid.ItemsSource = _userService.GetAllUsers(); // Gán danh sách user vào DataGrid
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
