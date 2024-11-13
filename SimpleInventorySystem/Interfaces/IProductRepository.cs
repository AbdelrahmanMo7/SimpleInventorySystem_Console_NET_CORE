using SimpleInventorySystem.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByNameAsync(string name);
        Task<IEnumerable<Product> > GetByCategoryAsync(int Category_Id);

    }
}
