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
    public class CategoryRepository : Repository<Category> ,ICategoryRepository
    {

        public CategoryRepository(InventoryContext _Context):base(_Context)
        {
            Console.WriteLine("Category Repository");

        }

        // get particular category by it's name so the function will check if the named category is found :
        // if the result is true then the category will be returned and if false the function will return null
        public async Task<Category> GetByNameAsync(string name)
        {
            //Using LINQ to get category by it's name.
            Category category = await this.List.FirstOrDefaultAsync(p => p.Category_Name!=null&& p.Category_Name.ToLower()==name.ToLower());

            return category;
        }

    }
}
