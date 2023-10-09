using calculaJurosApi.Interfaces;
using calculaJurosApi.Models.Request;
using Microsoft.AspNetCore.Mvc;


namespace calculaJurosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculaJurosApiController : ControllerBase
    {

        [HttpPost("calculajuros")]
        public async Task<IActionResult> CalculaJurosAsync([FromServices] ITaxaServices taxaServices, [FromQuery] CalculaJurosRequest calculaJurosRequest)
        {
            try
            {
                var valorTotal = await taxaServices.GetValorTotalAsync(calculaJurosRequest.ValorInicial, calculaJurosRequest.Meses);
                return Ok(valorTotal);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("showmethecode")]
        public String ShowMeTheCode()
        {
            return "https://github.com/eldercnw/granito-teste";
        }
    }
}
