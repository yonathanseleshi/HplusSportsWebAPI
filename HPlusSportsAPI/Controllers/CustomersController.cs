using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;
using HPlusSportsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller

    {

        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        // GET api/customers
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var results = new ObjectResult( _customerRepository.GetAll())
            {

                StatusCode = (int)HttpStatusCode.OK

            };

            
          Request.HttpContext.Response.Headers.Add("Total Count", _customerRepository.Count().ToString());

            return results;
        }

        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task <IActionResult> GetCustomer(int id)
        {

            var customer = _customerRepository.Find(id);

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

           await _customerRepository.Add(customer);

            return CreatedAtAction("GetCustomer", new {id = customer.CustomerId}, customer);
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody] Customer customer)
        {

            

            return Ok(customer);
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {

            var customer = _customerRepository.Remove(id);

            return Ok(customer);
        }
    }
}