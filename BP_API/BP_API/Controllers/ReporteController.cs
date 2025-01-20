using BP_API.Contracts;
using BP_API.DTO;
using BP_API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporte _reporteService;

        public ReporteController(IReporte reporteService)
        {
            _reporteService = reporteService;
        }
        [HttpPost("")]
        public async Task<IActionResult> GenerarEstadoCuenta([FromBody] ReporteEstadoCuentaParametrosDTO parametros)
        {
            try
            {
                var reporte = await _reporteService.GenerarEstadoCuentaAsync(parametros);

                // Generar PDF en base64
                var pdfBase64 = await _reporteService.GenerarPdfEstadoCuentaAsync(reporte);

                return Ok(new
                {
                    Json = reporte,
                    PdfBase64 = pdfBase64
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
