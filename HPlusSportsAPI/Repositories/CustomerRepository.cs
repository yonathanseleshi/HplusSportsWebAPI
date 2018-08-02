using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportsAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly HPlusSportsContext _context;

        public CustomerRepository(HPlusSportsContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
    

            return _context.Customer;
        }

        public async Task<Customer> Find(int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(x => x.CustomerId == id);

            return customer;
        }

        public async Task<Customer> Add(Customer customer)
        {
         

            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;

        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(x => x.CustomerId == id);

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<int> Count()
        {
            var count = await _context.Customer.CountAsync();

            return count;
        }
    }
}
