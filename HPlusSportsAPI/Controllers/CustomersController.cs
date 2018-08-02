using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly HPlusSportsContext _context;


        public CustomersController(HPlusSportsContext context)
        {
            _context = context;
        }


        // GET api/customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var results = new ObjectResult(_context.Customer)
            {

                StatusCode = (int) HttpStatusCode.OK

            };

          Request.HttpContext.Response.Headers.Add("Total Count", _context.Customer.CountAsync().ToString());

            return results;
        }

        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task <IActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync( x => x.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new {id = customer.CustomerId}, customer);
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody] Customer customer)
        {

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {

            var customer = await _context.Customer.SingleOrDefaultAsync(x => x.CustomerId == id);

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }
    }
}