using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebApi.Integration.Tests.Setup
{
    public abstract class BaseTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly HttpClient Client;
        protected readonly CustomWebApplicationFactory<Startup> Factory;

        protected BaseTest(CustomWebApplicationFactory<Startup> factory)
        {
            Factory = factory;
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
