using BP_API.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralesController : ControllerBase
    {
        private readonly IParametro _parametro;

        public GeneralesController(IParametro parametro)
        {
            _parametro = parametro;
        }

        [HttpGet("GetParmetros")]
        public IActionResult GetParmetros(int id)
        {
            var result = _parametro.GetParametros(id);
            return new JsonResult(result);
        }
        [HttpGet("GetParmetroById")]
        public IActionResult GetParmetroById(int id)
        {
            var result = _parametro.GetDescripcionParametrosById(id);
            return new JsonResult(result);
        }
    }
}
