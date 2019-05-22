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

namespace InventoryManager
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Model1 data = new Model1();

        public Window2()
        {
            InitializeComponent();
            parent.ItemsSource = data.Categories.Select(x => x.Name.TrimEnd()).ToList();
        }

        private void Save_sub_Click(object sender, RoutedEventArgs e)
        {
            if (!data.Subcategories.Any(x => x.Name == sub_name.Text) || !data.Categories.Any(x => x.Name == sub_name.Text))
            {
                Subcategory subcategory = new Subcategory()
                {
                    Name = sub_name.Text.TrimEnd(),
                    Description = sub_desc.Text,
                    Category_id = data.Categories.First(x => x.Name == parent.SelectedValue.ToString()).Id,
                    Category = data.Categories.First(x => x.Name == parent.SelectedValue.ToString())
                };

                data.Subcategories.Add(subcategory);
                data.SaveChanges();
                TreeViewItem treeView = new TreeViewItem();
                treeView.Header = subcategory.Name.TrimEnd();
                treeView.ToolTip = subcategory.Description;
                int index = MainWindow.listBox.Items.IndexOf(data.Categories.First(x => x.Name == parent.SelectedValue.ToString()).Name);
                TreeViewItem parent_item = new TreeViewItem();
                foreach (var item in MainWindow.listBox.Items)
                {
                    TreeViewItem b = (TreeViewItem)item;
                    if (b.Header.ToString() == parent.SelectedValue.ToString())
                    {
                        parent_item = b;
                        parent_item.Items.Add(treeView);
                        
                    }
                }
                this.Close();
                
            } else
            {
                MessageBox.Show("Subcategory already exists or a category with that name exists");
            }

        }
    }
}
