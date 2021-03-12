using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository
{
    public interface IProductsOrdersRepository
    {
        public Task<List<ProductOrders>> GetProductOrdersAsync();

        void UpdateProductOrders(ProductOrders productOrders);
        void DeleteProductOrders(ProductOrders productOrders);
    }
}