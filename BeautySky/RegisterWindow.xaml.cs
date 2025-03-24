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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserService _service = new();
        private RoleService _role = new();
       public User EditedOne { get; set; }

        public RegisterWindow()
        {
            InitializeComponent();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RoleIDComboBox.ItemsSource = _role.GetAllRoles();
            RoleIDComboBox.DisplayMemberPath = "RoleName"; 
            RoleIDComboBox.SelectedValuePath = "RoleId"; 

            if (EditedOne == null)
            {
                DetailWindowModeLabel.Content = "Tạo mới user (Create)";
                return;
            }

            DetailWindowModeLabel.Content = "Cập nhật thông tin user (Update)";

            UserNameTextBox.Text = EditedOne.UserName;
            FullnameTextBox.Text = EditedOne.FullName;
            EmailTextBox.Text = EditedOne.Email;
            PhoneTextBox.Text = EditedOne.Phone;

            RoleIDComboBox.SelectedValue = EditedOne.RoleId;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = new();
            user.UserName = UserNameTextBox.Text;
            user.FullName = FullnameTextBox.Text;
            user.Email = EmailTextBox.Text;
            user.Password = PasswordBox.Password;
            user.ConfirmPassword = ConfirmPasswordBox.Password;
            user.Phone = PhoneTextBox.Text;
            user.RoleId = int.Parse("1"); // Nếu chắc chắn giá trị luôn hợp lệ


            _service.CreateUser(user);
            this.Close();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();       
        }
    }
}
