using Moq;
using Moq.Protected;
using Xunit;
using calculaJurosApi.Models.Response;
using calculaJurosApi.Services;

namespace CalculosUnitTest
{
    public class TaxaServiceTest
    {
        [Fact]
        public async Task GetValorTotalAsync_ShouldReturnValueAsync()
        {
            var jurosResponseMock = new JurosResponse()
            {
                Juros = 0.01
            };

            var mockHandler = HttpClientHelper
                .GetResults<JurosResponse>(jurosResponseMock);

            var mockHttpClient = new HttpClient(mockHandler.Object);
            mockHttpClient.BaseAddress = new Uri("http://localhost:1398/api");

            var taxaServices = new TaxaServices(mockHttpClient);

            var valorInicial = 20.0M;
            var meses = 2;


            var valorTotal = await taxaServices.GetValorTotalAsync(valorInicial, meses);


            Assert.NotNull(valorTotal);
            Assert.Equal(20.40M, valorTotal);

            mockHandler
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get &&
                    req.RequestUri == new Uri("http://localhost:1398/api/jurosApi/juros")),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task GetValorTotalAsync_ShouldReturnErrorAsync()
        {
            var jurosResponseMock = new JurosResponse()
            {
                Juros = 0.01
            };

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new HttpRequestException("Erro na requisi��o HTTP"));

            var mockHttpClient = new HttpClient(mockHandler.Object);
            mockHttpClient.BaseAddress = new Uri("http://localhost:1398/api");

            var taxaServices = new TaxaServices(mockHttpClient);

            var valorInicial = 20.0M;
            var meses = 2;


            await Assert.ThrowsAsync<HttpRequestException>(() => taxaServices.GetValorTotalAsync(valorInicial, meses));


        }
    }
}