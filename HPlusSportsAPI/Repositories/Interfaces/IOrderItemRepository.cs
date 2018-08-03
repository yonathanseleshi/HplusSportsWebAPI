using System.Collections.Generic;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;

namespace HPlusSportsAPI.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> Add(OrderItem item);

        IEnumerable<OrderItem> GetAll();

        Task<OrderItem> Find(int id);

        Task<OrderItem> Remove(int id);

        Task<OrderItem> Update(OrderItem item);

        Task<bool> Exists(int id);
    }
}