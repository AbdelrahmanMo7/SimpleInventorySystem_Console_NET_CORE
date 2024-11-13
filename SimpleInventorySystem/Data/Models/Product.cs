using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.DataLayer.Models
{
    public class Product
    {
        // product Entity Properties 
        public int Id { get; set; }

        // name must be unique
        public string Product_Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public int Category_ID { get; set; }
        public virtual Category Category { get; set; }

        // override the tostring() to display product properties
        public override string ToString()
        {
            return $"ID: {this.Id} ,  Product name : {this.Product_Name} , Category : {this.Category.Category_Name} ->  Price : {this.Price:C} , Stock Quantity : {this.StockQuantity}";
        }

    }
}
