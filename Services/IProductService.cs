
using System.Threading.Tasks;
using Entities.Models;

namespace Services
{
    public interface IProductService
    {
        Task UpdateProduct(int productId, Product product);

    }
}