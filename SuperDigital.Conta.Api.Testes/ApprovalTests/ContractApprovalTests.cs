using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace SuperDigital.Conta.Api.Testes.ApprovalTests
{
    public sealed class ContractApprovalTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        private readonly HttpClient _httpClient;

        public ContractApprovalTests(WebApplicationFactory<Startup> applicationFactory)
        {
            applicationFactory = applicationFactory.WithWebHostBuilder(webBuilder =>
            {

            });

            applicationFactory.Server.AllowSynchronousIO = true;
            _httpClient = applicationFactory.CreateClient();
        }

        [Fact]
        public async Task SwaggerContract()
        {
            // Arrange

            // Act
            var json = await _httpClient.GetStringAsync("/internal/swagger/v1/swagger.json");

            // Assert
            Approvals.VerifyJson(json);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

}
