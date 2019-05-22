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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Model1 entities = new Model1();

        public Window1()
        {
            InitializeComponent();
        }

        private void Add_category_Click(object sender, RoutedEventArgs e)
        {
            if(entities.Categories.Any(x=>x.Name.ToLower() == category_name.Text.ToLower()))
            {
                MessageBox.Show("Category already exists");
            } else
            {
                Category category = new Category()
                {
                    Name = category_name.Text.TrimEnd(),
                    Description = category_description.Text
                };

                entities.Categories.Add(category);
                entities.SaveChanges();
                TreeViewItem newExpander = new TreeViewItem();
                newExpander.Header = category.Name.TrimEnd();
                newExpander.ToolTip = category.Description;
                MainWindow.listBox.Items.Add(newExpander);
                this.Close();
            }          
        }
    }
}
