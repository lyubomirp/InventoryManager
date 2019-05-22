using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryManager
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        Model1 data = new Model1();

        public AddItem()
        {
            InitializeComponent();
            categories_box.ItemsSource = data.Categories.Select(x=>x.Name.TrimEnd()).ToList();
            subcategory_container.ItemsSource = data.Subcategories.Select(x => x.Name.TrimEnd()).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (categories_box.SelectedValue == null || subcategory_container.SelectedValue == null)
            {
                MessageBox.Show("You need to assign a category and subcategory");
            }else if (data.Items.Any(x=>x.Name.ToLower() == item_name.Text.TrimEnd().ToLower()))
            {
                MessageBox.Show("That item already exists");   
            }else
            {
                Item newItem = new Item()
                {
                    Name = item_name.Text.TrimEnd(),
                    Description = item_desc.Text,
                    Type = categories_box.SelectedValue.ToString().TrimEnd(),
                    Price = float.Parse(item_price.Text),
                    Subcategory = data.Subcategories.FirstOrDefault(x=>x.Name == (string)subcategory_container.SelectedValue),
                    Quantity = int.Parse(item_quantity.Text),
                    Category = data.Categories.FirstOrDefault(x => x.Name == (string)categories_box.SelectedValue)
                };

                data.Categories.FirstOrDefault(x => x.Name == (string)categories_box.SelectedValue).Items.Add(newItem);
                data.Subcategories.FirstOrDefault(x => x.Name == (string)subcategory_container.SelectedValue).Items.Add(newItem);

                data.Items.Add(newItem);
                data.SaveChanges();
                MainWindow.itemsContainer.Add(newItem);
                this.Close();
            }           
        }

        private void Item_quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(item_quantity.Text, @"^\d+$"))
            {
                MessageBox.Show("Quantity needs to be a number");
                item_quantity.Text = "0";
            }
        }

        private void Item_price_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(item_price.Text, @"^[0-9]+(\.[0-9]+)?$") && !Regex.IsMatch(item_price.Text, @"^\d+$"))
            {
                MessageBox.Show("Price need to be a number");
                item_price.Text = "0.00";
            }
        }

        private void Categories_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string category_name = (string)categories_box.SelectedValue;

            subcategory_container.ItemsSource = data.Subcategories.Where(x => x.Category.Name == category_name).Select(x=>x.Name.TrimEnd()).ToList();
        }



        //private void Item_price_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!Regex.IsMatch(item_quantity.Text, @"^(\d+)?"))
        //    {
        //        MessageBox.Show("Quantity cannot contain letters");=
        //        item_quantity.Text = "";
        //    }

        //}

        //private void Item_quantity_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!Regex.IsMatch(item_price.Text, @"^(\d+)\.(\d+)?") || !Regex.IsMatch(item_price.Text, @"^(\d+)?"))
        //    {
        //        MessageBox.Show("Price cannot contain letters");
        //        item_price.Text = "";
        //    }
        //}
    }
}
