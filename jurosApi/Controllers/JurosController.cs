using Microsoft.AspNetCore.Mvc;
using taxas.Models.Response;

namespace taxas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JurosApiController : ControllerBase
    {

        [HttpGet("juros")]
        public JurosResponse Get()
        {
            return new JurosResponse()
            {
                Juros = 0.01
            };
        }
    }
}
