using System.Threading.Tasks;
using Entities.Data;

namespace Repository.impl
{
    public class RepositoryManager: IRepositoryManager
    {
        private OrderDbContext _orderDbContext;
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;
        private IProductRepository _productRepository;

        public RepositoryManager(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public IOrderRepository Order
        {
            get { return _orderRepository ??= new OrderRepository(_orderDbContext); }
        }
        public ICustomerRepository Customer
        {
            get { return _customerRepository ??= new CustomerRepository(_orderDbContext); }
        }
        public IProductRepository Product
        {
            get { return _productRepository ??= new ProductRepository(_orderDbContext); }
        }

        public Task SaveAsync() => _orderDbContext.SaveChangesAsync();
    }
}