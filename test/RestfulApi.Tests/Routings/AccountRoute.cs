// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using System.Net;
using System.Net.Http;
using Xunit;

namespace RestfulApi.Tests.Routings
{
    public class AccountRoute
    {
        public AccountRoute(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }

        [Fact]
        public async void AccountController()
        {
            var response = await Client.GetAsync("http://localhost/api/account");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}