using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    [Table("Items")]
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }

        [ForeignKey("Subcategory")]
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        [ForeignKey("Category")]
        public int Category_id { get; set; }
        public Category Category { get; set; }

    }
}
