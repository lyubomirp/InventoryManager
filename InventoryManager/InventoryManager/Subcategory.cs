using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    [Table("Subcategories")]
    public class Subcategory
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<Item> Items { get; set; }

        [ForeignKey("Category")]
        public int Category_id { get; set; }
        public Category Category { get; set; }

        public Subcategory()
        {
            Items = new List<Item>();
        }

    }
}
