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
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            mockRepo.Setup(m => m.GetAll()).Returns(Task.FromResult(GetCustomers()));

      
            var customersController = new CustomersController(mockRepo.Object);

            var response = customersController.GetCustomer();
            

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));
           

        }

    }
}
