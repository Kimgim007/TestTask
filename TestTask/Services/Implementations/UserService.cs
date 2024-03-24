using Microsoft.Extensions.Hosting;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using System.Linq;
namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _applicationDbContext;
        private IOrderService _orderService;
        public UserService(ApplicationDbContext applicationDbContext, IOrderService orderService)
        {
            this._applicationDbContext = applicationDbContext;
            this._orderService = orderService;
        }

        public Task<User> GetUser()
        {
            var userId = _applicationDbContext.Orders.GroupBy(order => order.UserId).OrderByDescending(q => q.Count()).Select(group => group.Key).FirstOrDefault();
            var user = _applicationDbContext.Users.FirstOrDefault(q => q.Id == userId);

            return Task.FromResult(user); ;
        }
        public Task<List<User>> GetUsers()
        {

            var users = _applicationDbContext.Users.Where(q => q.Status == Enums.UserStatus.Inactive).ToList();

            return Task.FromResult(users);
        }
    }
}
