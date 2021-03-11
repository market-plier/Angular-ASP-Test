using System.Threading.Tasks;
using Angular_ASP_Test.Models;

namespace Angular_ASP_Test.Services
{
    public interface IProductService
    {
        Task UpdateProduct(int productId, Product product);

    }
}