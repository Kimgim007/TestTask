using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext _applicationDbContext;
        public OrderService(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public Task<Order> GetOrder()
        {
            var orders = _applicationDbContext.Orders.OrderByDescending(order => order.Price * order.Quantity);
            return Task.FromResult(orders.FirstOrDefault());
        }
      
        public Task<List<Order>> GetOrders()
        {
            var orders = _applicationDbContext.Orders.Where(q => q.Quantity > 10).ToList();
            return Task.FromResult(orders);
        }
    }
}
