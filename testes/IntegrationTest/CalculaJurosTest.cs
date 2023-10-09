using System.Net;
using RichardSzalay.MockHttp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using calculaJurosApi.Interfaces;
using Moq;
using Newtonsoft.Json;
using System.Text.Json;
using calculaJurosApi.Models.Response;
using JsonSerializer = System.Text.Json.JsonSerializer;
using calculaJurosApi.Models.Request;

namespace CalculaJurosTest;
public class CalculaJurosApiControllerIntegrationTests
{
    [Fact]
    public async Task Test_CalculaJurosAsync_Sucesso()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://localhost:32800/api/JurosApi/juros")
            .Respond(HttpStatusCode.OK, "application/json", "{ \"juros\": 0.1 }");

        var client = new HttpClient(mockHttp);

        var builder = WebApplication.CreateBuilder(new string[] { });
        var app = builder.Build();

        var httpClient = new HttpClient(mockHttp);

        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:32798/api/CalculaJurosApi/calculajuros?ValorInicial=1&Meses=10");
        var response = await httpClient.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("112.68", content);

    }
}