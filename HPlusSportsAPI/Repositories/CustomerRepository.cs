﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;
using HPlusSportsAPI.Repositories.Interfaces;

namespace HPlusSportsAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private HPlusSportsContext _context;

        public CustomerRepository(HPlusSportsContext context)
        {
            _context = context;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Customer.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Find(int id)
        {
            return await _context.Customer.Include(customer => customer.Order).SingleOrDefaultAsync(a => a.CustomerId == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customer;
        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _context.Customer.SingleAsync(a => a.CustomerId == id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
