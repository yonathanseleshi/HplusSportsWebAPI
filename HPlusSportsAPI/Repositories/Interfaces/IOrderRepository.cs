﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;

namespace HPlusSportsAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order item);

        IEnumerable<Order> GetAll();

        Task<Order> Find(int id);

        Task<Order> Remove(int id);

        Task<Order> Update(Order item);

        Task<bool> Exists(int id);
    }
}