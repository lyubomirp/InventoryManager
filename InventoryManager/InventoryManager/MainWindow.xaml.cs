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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        Model1 data = new Model1();
        public static TreeView listBox;
        public static List<Item> itemsContainer;

        public MainWindow()
        {
            InitializeComponent();
            
            foreach (var category in data.Categories.Select(x => x.Name.TrimEnd()).ToList())
            {
                var id = data.Categories.First(x => x.Name == category).Id;
                List<Subcategory> subcategories = data.Subcategories.Where(x => x.Category_id == id).ToList();
                TreeViewItem treeViewItem = new TreeViewItem();
                treeViewItem.Header = category.TrimEnd();
                treeViewItem.ToolTip = data.Categories.First(x => x.Id == id).Description;
                treeViewItem.Selected += new RoutedEventHandler(Categories_grid_SelectionChanged);
                
                foreach (var sub in subcategories)
                {
                    TreeViewItem subs = new TreeViewItem();
                    subs.Header= sub.Name.TrimEnd();
                    subs.ToolTip = sub.Description;
                    treeViewItem.Items.Add(subs);
                }
                
            categories_grid.Items.Add(treeViewItem);
            }
            //categories_grid.ItemsSource = data.Categories.Select(x => x.Name).ToList();
            itemsContainer = data.Items.ToList();
            listBox = categories_grid;
            subcategory_container.ItemsSource = data.Subcategories.Select(x=>x.Name.TrimEnd()).ToList();
            foreach (var item in data.Categories.Select(x => x.Name.TrimEnd()).ToList())
            {
                current_item_type.Items.Add(item);
            }
        }


        private void Add_category_Click(object sender, RoutedEventArgs e)
        {
            Window1 category_page = new Window1();
            category_page.ShowDialog();
        }

        private void Categories_grid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            LoadItems();
            SwapSubcategories();
        }

        public void LoadItems()
        {
            List<Border> toView = new List<Border>();
            itemsContainer = data.Items.ToList();

            foreach (var item in itemsContainer.Where(x => x.Type == listBox.SelectedValue.GetType()
            .GetProperty("Header").GetValue(listBox.SelectedValue).ToString()))
            {
                toView.Add(InitializeItemContainers(item));

            }

            items_box.ItemsSource = toView;
            string name = listBox.SelectedValue.GetType()
            .GetProperty("Header").GetValue(listBox.SelectedValue).ToString();

            if (data.Categories.Any(x => x.Name == name))
            {
                Category category = data.Categories.FirstOrDefault(x => x.Name == name);

                toView = new List<Border>();
                foreach (var item in category.Items)
                {
                    toView.Add(InitializeItemContainers(item));
                }
                items_box.ItemsSource = toView;
            }
            else
            {
                Subcategory subcategory = data.Subcategories.FirstOrDefault(x => x.Name == name);

                toView = new List<Border>();
                foreach (var item in subcategory.Items)
                {
                    toView.Add(InitializeItemContainers(item));
                }
                items_box.ItemsSource = toView;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddItem addItem = new AddItem();
            addItem.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {

            try
            {
                    CheckCategories();
                    LoadItems();
                    SwapSubcategories();

            }
            catch (Exception ex) { }

        }

        private void CheckCategories()
        {
            if (current_item_type.Items.Count < data.Categories.Select(x => x.Name.TrimEnd()).ToList().Count)
            {
                current_item_type.Items.Add(data.Categories.Select(x => x.Name.TrimEnd()).ToList().Last());
            }

        }

        private void Items_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Border selectedBorder = (Border)items_box.SelectedValue;

                StackPanel selectedPanel = (StackPanel)selectedBorder.Child;

                Label selectedLabel = selectedPanel.Children.OfType<Label>().First();

                string selectedItem = selectedLabel.Content.ToString();


                string name = selectedItem.TrimEnd();

                Item currentItem = data.Items.First(x => x.Name.TrimEnd() == name);

                SwapSubcategories(currentItem);


                current_item_name.Text = currentItem.Name.TrimEnd().TrimStart();
                current_item_price.Text = currentItem.Price.ToString();
                current_item_description.Text = currentItem.Description;
                current_item_quantity.Text = currentItem.Quantity.ToString();
                current_item_type.SelectedValue = currentItem.Type.TrimEnd();
                current_item_selection.Content = currentItem.Id;
            }
            catch (Exception nre)
            {
                current_item_name.Text = "";
                current_item_price.Text = "";
                current_item_description.Text = "";
                current_item_quantity.Text = "";
                subcategory_container.SelectedValue = "";
                current_item_type.SelectedValue = "";
                current_item_selection.Content = string.Empty;
            }
        }

        

        private void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (current_item_selection.Content.ToString() != "Label")
            {
                Item toUpdate = data.Items.First(x => x.Id == (int)current_item_selection.Content);

                if (toUpdate != null)
                {
                    toUpdate.Name = current_item_name.Text;
                    toUpdate.Price = float.Parse(current_item_price.Text);
                    toUpdate.Description = current_item_description.Text;
                    toUpdate.Quantity = int.Parse(current_item_quantity.Text);
                    toUpdate.Type = data.Categories.First(x => x.Name.TrimEnd() == (current_item_type.SelectedValue.ToString().TrimEnd())).Name.TrimEnd();
                    toUpdate.Subcategory = data.Subcategories.First(x => x.Name.TrimEnd() == subcategory_container.SelectedValue.ToString().TrimEnd());
                    toUpdate.SubcategoryId = data.Subcategories.First(x => x.Name.TrimEnd() == subcategory_container.SelectedValue.ToString().TrimEnd()).Id;
                    toUpdate.Category = data.Categories.First(x => x.Name.TrimEnd() == current_item_type.SelectedValue.ToString().TrimEnd());
                    toUpdate.Category_id = data.Categories.First(x => x.Name.TrimEnd() == (current_item_type.SelectedValue.ToString().TrimEnd())).Id;

                    data.SaveChanges();
                    LoadItems();
                    SwapSubcategories();
                }

            }
            else
            {
                MessageBox.Show("You haven't selected a product");
            }
        }

        private void Add_subCategory_Click(object sender, RoutedEventArgs e)
        {
            Window2 subcategory_page = new Window2();
            subcategory_page.ShowDialog();
        }

        private void Delete_current_item(object sender, RoutedEventArgs e)
        {
            if (current_item_selection.Content.ToString() != "Label")
            {
                Item toDelete = data.Items.First(x => x.Id == (int)current_item_selection.Content);
                if (toDelete != null)
                {
                    data.Items.Remove(toDelete);

                    data.SaveChanges();
                    LoadItems();
                }
            }
        }

        private void Current_item_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SwapSubcategories();
        }

        private void SwapSubcategories()
        {
            string category_name = (string)current_item_type.SelectedValue;
            subcategory_container.ItemsSource = data.Subcategories.Where(x => x.Category.Name == category_name).Select(x => x.Name.TrimEnd()).ToList();
        }

        private void SwapSubcategories(Item currentItem)
        {
            string category_name = (string)current_item_type.SelectedValue;
            subcategory_container.ItemsSource = data.Subcategories.Where(x => x.Category.Name == category_name).Select(x => x.Name.TrimEnd()).ToList();
            subcategory_container.SelectedValue = currentItem.Subcategory.Name.TrimEnd();
        }

        private void Current_item_quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(current_item_quantity.Text, @"^\d+$"))
            {
                MessageBox.Show("Quantity needs to be a number");
                current_item_quantity.Text = "0";
            }
        }

        private void Current_item_price_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(current_item_price.Text, @"^[0-9]+(\.[0-9]+)?$") && !Regex.IsMatch(current_item_price.Text, @"^\d+$"))
            {
                MessageBox.Show("Price need to be a number");
                current_item_price.Text = "0.00";
            }
        }

        private void Current_item_name_LostFocus(object sender, RoutedEventArgs e)
        {
            string name = current_item_name.Text;

            if(data.Items.Any(x=>x.Name == name) || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is invalid or already exists");
            }
        }

        private Border InitializeItemContainers(Item item)
        {
            Border border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 1, 1, 1);

            StackPanel stackPanel = new StackPanel();
            stackPanel.Height = 110;
            stackPanel.Width = 119;
            stackPanel.Background = Brushes.LightGray;

            border.Child = stackPanel;

            Label headerLabel = new Label();
            headerLabel.HorizontalAlignment = HorizontalAlignment.Left;
            headerLabel.FontWeight = FontWeights.Bold;
            headerLabel.Content = item.Name;

            Label descLabel = new Label();
            descLabel.Content = "Description:";


            descLabel.HorizontalAlignment = HorizontalAlignment.Center;
            TextBlock description = new TextBlock();
            description.FontSize = 10;
            description.FontWeight = FontWeights.Bold;
            description.TextWrapping = TextWrapping.Wrap;
            description.Margin = new Thickness(10, 0, 0, 0);
            description.Text = item.Description;

            stackPanel.Children.Add(headerLabel);
            stackPanel.Children.Add(descLabel);
            stackPanel.Children.Add(description);

            TextBlock price = new TextBlock();
            price.FontSize = 10;
            price.FontWeight = FontWeights.Bold;
            price.TextWrapping = TextWrapping.Wrap;
            price.Margin = new Thickness(10, 0, 0, 0);
            price.Text = item.Description;
            price.Text = "Price: " + item.Price.ToString();

            stackPanel.Children.Add(price);

            TextBlock quantity = new TextBlock();
            quantity.FontSize = 10;
            quantity.FontWeight = FontWeights.Bold;
            quantity.TextWrapping = TextWrapping.Wrap;
            quantity.Margin = new Thickness(10, 0, 0, 0);
            quantity.Text = item.Description;
            quantity.Text = "Quantity: " + item.Quantity.ToString();

            stackPanel.Children.Add(quantity);

            

            return border;
        }
    }
}
