using BLL.BeautySky.Services;
using DAL.BeautySky.Models;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserService _service = new();
        
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string pass = PasswordTextBox.Text;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Both email and password are required!", "Fields required", MessageBoxButton.OK, MessageBoxImage.Error);
                return; //early return, ko cần viết else
            }
            User? acc = _service.Authenticate(email, pass);

            if (acc == null)
            {
                MessageBox.Show("Invalid email or password!", "Wrong credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return; //early return, ko cần viết else
            }


            if (acc.RoleId == 3) //MANGER CẤM CỬA - PHÂN QUYỀN VÀO APP KHÁC
            {
                ManagerWindow manager = new();
                manager.Show();
                this.Hide();

            }

            MainWindow main = new();
            main.AuthenticatedUser = acc; //GỬI ACC VỪA LOGIN THÀNH CÔNG SANG KIA LÀM BIẾN PHẤT CỜ - MODE NÀO???
            main.Show();
            this.Hide();  //show Main và ẩn Login phía sau
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
           
                RegisterWindow registerWindow = new RegisterWindow();
                registerWindow.Show();

        }
    }
}
