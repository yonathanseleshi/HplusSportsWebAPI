using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;

namespace HPlusSportsAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> Add(Customer customer);

        Task<List<Customer>> GetAll();

        Task<Customer> Find(int id);

        Task<Customer> Update(Customer customer);

        Task<Customer> Remove(int id);

        Task<bool> Exist(int id);
    }
}
