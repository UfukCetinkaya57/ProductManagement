using ProductManagement.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync(); 
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<IEnumerable<Product>> GetByCriteriaAsync(string name, decimal? minPrice, decimal? maxPrice);
    }
}
