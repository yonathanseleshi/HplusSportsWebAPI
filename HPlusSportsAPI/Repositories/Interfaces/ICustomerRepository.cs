using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;

namespace HPlusSportsAPI.Repositories
{
    public interface ICustomerRepository
    {

        IEnumerable<Customer> GetAll();

        Task<Customer> Find(int id);

        Task<Customer> Add(Customer customer);

        Task<Customer> Remove(int id);

        Task<Customer> Update(Customer customer);

        Task<int> Count();


    }
}
