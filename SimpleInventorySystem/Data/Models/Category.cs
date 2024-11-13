using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.DataLayer.Models
{
    public class Category
    {
        // Category properties
        public int Id { get; set; }
        // category name must be unique
        public string Category_Name { get; set; }
        public virtual ICollection<Product> products { get; set; }

      
        // override the tostring() to display Category properties
        public override string ToString()
        {
            return $"Category Name : {this.Category_Name} ";
        }
    }
}
