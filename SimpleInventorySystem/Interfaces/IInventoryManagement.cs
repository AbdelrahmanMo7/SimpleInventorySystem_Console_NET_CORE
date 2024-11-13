using SimpleInventorySystem.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.Interfaces
{
    public interface IInventoryManagement
    {
        Task AddNewProductAsync(Product product);
        Task AddNewCategoryAsync(string category_name);
        Task UpdateStockQuantityAsync(string product_name, int NewQuantity);
        Task DisplayAllCategoriesAsync();
        Task DisplayProducts_ByCategoryAsync(int category_id);
        Task DisplayTotalInventoryAsync();
    }
}
