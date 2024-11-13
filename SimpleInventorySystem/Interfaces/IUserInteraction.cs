using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.Interfaces
{
    public interface IUserInteraction
    {
        Task DisplaySystemOptionsAsync();
        Task DisplayCategoriesAsync();
        Task DisplayProductsByCategoryAsync();
        Task DisplayTotalInventoryAsync();
        Task AddingNewCategoryAsync();
        Task AddingNewProductAsync();
        Task UpdatingStockQuantityAsync();
    }
}
