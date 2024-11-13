using Microsoft.EntityFrameworkCore;
using SimpleInventorySystem.DataLayer;
using SimpleInventorySystem.DataLayer.Models;
using SimpleInventorySystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.DataAccesslogic
{
    public class ProductRepository : Repository<Product> ,IProductRepository
    {

        public ProductRepository(InventoryContext _Context):base(_Context)
        {
        }
        // get all products within particular category and ignore the case sensitive in compairing category name in Equals Function
        //and if the list is empty or the category not found the function will throw an exception to be handled
        public async Task<IEnumerable<Product>> GetByCategoryAsync(int Category_Id)
        {
            //Using LINQ to list products by category id.
            IEnumerable<Product> FilteredList= await this.List.Where(p=>p.Category.Id==Category_Id).ToListAsync();
            if ( FilteredList.Any())
                return FilteredList;
            else 
                throw new Exception($" there is no products in the Iventory for this category");


        }

        // get particular product by it's name so the function will check if the named product is found :
        // if the result is true then the product will be returned and if false the function will return null
        public async Task<Product> GetByNameAsync(string name)
        {
            //Using LINQ to get product by it's name.
            Product product = await this.List.FirstOrDefaultAsync(p => p.Product_Name.ToLower() == name.ToLower());

            return product;
        }
    }
}
