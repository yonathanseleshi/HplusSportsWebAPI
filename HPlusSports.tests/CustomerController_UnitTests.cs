using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HPlusSportsAPI.Controllers;
using HPlusSportsAPI.Models;
using HPlusSportsAPI.Repositories;
using HPlusSportsAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace HPlusSports.tests
{
    [TestClass]
    public class CustomerController_UnitTests
    {


        private List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();

            customers.Add(new Customer()
            {
                CustomerId = 1,
                FirstName = "John",
                LastName = "Tester",
                Email = "john@tester.com"

            });


            customers.Add(new Customer()
            {
                CustomerId = 2,
                FirstName = "James",
                LastName = "Tester",
                Email = "james@tester.com"

            });

            return customers;

        }

        [TestMethod]
        public void GetCustomers_ReturnsAllCustomers()
        {

            

            var mockRepo = new Mock<ICustomerRepository>();

            var logger = Mock.Of<ILogger<CustomersController>>();


            mockRepo.Setup(m => m.GetAll()).Returns(Task.FromResult(GetCustomers()));

      
            var customersController = new CustomersController(mockRepo.Object, logger);

            var response = customersController.GetCustomer();
            

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));
           

        }

        [TestMethod]
        public void PostCustomer_ReturnBadRequest_IfModelIsInvalid()
        {


            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(m => m.GetAll()).Returns(Task.FromResult(GetCustomers()));

            var logger = Mock.Of<ILogger<CustomersController>>();


            var customersController = new CustomersController(mockRepo.Object, logger);

            customersController.ModelState.AddModelError("FirstName", "Required");

            var customer = new Customer()
            {
                CustomerId = 4,

            };

            var response = customersController.PostCustomer(customer);

            Assert.IsInstanceOfType(response.Result, typeof(BadRequestObjectResult));

        }
    }
}
