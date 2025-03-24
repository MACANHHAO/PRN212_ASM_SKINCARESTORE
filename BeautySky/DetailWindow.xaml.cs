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
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private ProductService _service = new();
        private CategoryService _cat = new();
        public Product EditedOne { get; set; }
        public DetailWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CategoryIdComboBox.ItemsSource = _cat.GetAllCategory();
            CategoryIdComboBox.DisplayMemberPath = "CategoryName"; //cột show ra
            CategoryIdComboBox.SelectedValuePath = "CategoryId"; //cột lấy value làm fk

            if (EditedOne == null)
            {
                DetailWindowModeLabel.Content = "Tạo mới máy lạnh (Create)";
                return;  //= null mode create, thoat ko làm gì cả, show màn hình trắng
            }


            //SỐNG SÓT ĐẾN ĐÂY LÀ EDIT!!!
            DetailWindowModeLabel.Content = "Cập nhật thông tin máy lạnh (Update)";

            //disable ô nhập mã số máy lạnh


            ProductNameTextBox.Text = EditedOne.ProductName;
            PriceTextBox.Text = EditedOne.Price.ToString("F2"); 
            DescriptionTextBox.Text = EditedOne.Description.ToString();
            IngredientTextBox.Text = EditedOne.Ingredient.ToString();
            CategoryIdComboBox.SelectedValue = EditedOne.CategoryId ;

            SkinTypeIdTextBox.Text = EditedOne.SkinTypeId.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Product obj = new();
            obj.ProductName = ProductNameTextBox.Text;
            obj.Price = Convert.ToDecimal(PriceTextBox.Text);
            obj.Quantity = int.Parse(QuantityTextBox.Text);
            
            obj.Description = DescriptionTextBox.Text;
            obj.Ingredient = IngredientTextBox.Text;

            obj.CategoryId = (int)CategoryIdComboBox.SelectedValue;

            obj.SkinTypeId = int.Parse(SkinTypeIdTextBox.Text);


            _service.CreateProduct(obj);


            this.Close();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Rectangle_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
