using BP_API.Contracts;
using BP_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientos _movimientosService;

        public MovimientosController(IMovimientos movimientosService)
        {
            _movimientosService = movimientosService;
        }

        [HttpGet("GetAllMovimientos")]
        public async Task<IActionResult> GetAllMovimientos()
        {
            var movimientos = await _movimientosService.GetAllMovimientosAsync();
            return Ok(movimientos);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movimiento = await _movimientosService.GetByIdAsync(id);
            if (movimiento == null) return NotFound();

            return Ok(movimiento);
        }

        [HttpPost("CreateMovimiento")]
        public async Task<IActionResult> Create([FromBody] MovimientoDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var movimiento = await _movimientosService.CreateMovimientoAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = movimiento.IdMovimiento }, movimiento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MovimientoDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _movimientosService.UpdateMovimientoAsync(id, request);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _movimientosService.DeleteMovimientoAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
