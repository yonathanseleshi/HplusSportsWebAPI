using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;
using HPlusSportsAPI.Repositories.Interfaces;

namespace HPlusSportsAPI.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private HPlusSportsContext _context;

        public OrderItemRepository(HPlusSportsContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _context.OrderItem;
        }

        public async Task<OrderItem> Add(OrderItem orderItem)
        {
            await _context.OrderItem.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<OrderItem> Find(int id)
        {
            return await _context.OrderItem.Include(orderItem => orderItem.Order).Include(orderItem => orderItem.Product).SingleOrDefaultAsync(a => a.OrderItemId == id);
        }

        public async Task<OrderItem> Remove(int id)
        {
            var orderItem = await _context.OrderItem.SingleAsync(a => a.OrderItemId == id);
            _context.OrderItem.Remove(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<OrderItem> Update(OrderItem orderItem)
        {
            _context.OrderItem.Update(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.OrderItem.AnyAsync(e => e.OrderItemId == id);
        }
    }
}