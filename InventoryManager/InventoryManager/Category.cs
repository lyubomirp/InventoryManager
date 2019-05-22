using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<Item> Items { get; set; }

        public Category()
        {
            Items = new List<Item>();
        }

    }
}
