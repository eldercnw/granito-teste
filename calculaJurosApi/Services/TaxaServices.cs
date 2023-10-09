using calculaJurosApi.Interfaces;
using calculaJurosApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace calculaJurosApi.Services;

public class TaxaServices : ITaxaServices
{
    private readonly HttpClient _httpClient;
    public TaxaServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<decimal?> GetValorTotalAsync( decimal valorInicial, int meses)
    {
        string url = "http://localhost:1398/api";
        double juros = 0.0;
        decimal valorTotal = 0.0M;

        try
        {

            using (HttpResponseMessage response = await _httpClient.GetAsync($"{url}/jurosApi/juros"))

            using (HttpContent content = response.Content)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                string result = await content.ReadAsStringAsync();
                JurosResponse? jurosResponse = JsonSerializer.Deserialize<JurosResponse>(result, options);
                juros = jurosResponse?.Juros ?? 0.0;
                valorTotal = valorInicial * (decimal)Math.Pow((1 + juros), meses);
            }
        }
        catch (Exception error)
        {
            throw new HttpRequestException(error.Message);
        }

        var valorTruncado = Math.Truncate(100 * valorTotal) / 100;
        return valorTruncado;
    }
}

