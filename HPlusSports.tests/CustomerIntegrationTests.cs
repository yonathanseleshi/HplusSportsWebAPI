using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using HPlusSportsAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HPlusSports.tests
{
    [TestClass]
    public class CustomerIntegrationTests
    {

        private HttpClient _client;

        public CustomerIntegrationTests()
        {
           
            
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [TestMethod]
        public void CustomerGetAllTests()
        {

            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Customers");

            var response = _client.SendAsync(request).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }


        [TestMethod]
        [DataRow(100)]
        public void CustomerGetOneTest(int id)
        {

            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Customers/{id}");

            var response = _client.SendAsync(request).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
