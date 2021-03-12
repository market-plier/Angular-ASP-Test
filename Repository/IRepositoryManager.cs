using System.Threading.Tasks;

namespace Repository
{
    public interface IRepositoryManager
    {
        public IOrderRepository Order
        {
            get;
        }
        public ICustomerRepository Customer
        {
            get;
        }
        public IProductRepository Product
        {
            get;
        }

        public Task SaveAsync();
    }
}