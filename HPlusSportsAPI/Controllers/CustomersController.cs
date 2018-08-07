using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HPlusSportsAPI.Models;
using System.Net;
using HPlusSportsAPI.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using NLog.LayoutRenderers;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private ILogger<CustomersController> _logger;

        public CustomersController(ICustomerRepository customerRepository, ILogger<CustomersController> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        private async Task<bool> CustomerExists(int id)
        {
            return await _customerRepository.Exist(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Customer>))]
        public async Task<IActionResult> GetCustomer()
        {
            _logger.LogInformation("GetCustomer action called.  Get all customers from the database.");

            var results = await _customerRepository.GetAll();
            

            
            return Ok(results);
        }

        [HttpGet("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerRepository.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            try
            {
                await _customerRepository.Update(customer);
                return Ok(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
        }

        [HttpPost]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _customerRepository.Add(customer);

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (! await CustomerExists(id))
            {
                return NotFound();
            }

            await _customerRepository.Remove(id);

            return Ok();
        }
    }
}