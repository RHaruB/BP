using BP_API.Contracts;
using BP_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ICuenta _cuentasService;

        public CuentasController(ICuenta cuentasService)
        {
            _cuentasService = cuentasService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCuentas()
        {
            var cuentas = await _cuentasService.GetAllCuentasAsync();
            return Ok(cuentas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cuenta = await _cuentasService.GetByIdAsync(id);
            if (cuenta == null) return NotFound();

            return Ok(cuenta);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CuentaDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cuenta = await _cuentasService.CreateCuentaAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = cuenta.IdCuenta }, cuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CuentaDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _cuentasService.UpdateCuentaAsync(id, request);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _cuentasService.DeleteCuentaAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
