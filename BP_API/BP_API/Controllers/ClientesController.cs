using BP_API.Contracts;
using BP_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientes _clientesService;

        public ClientesController(IClientes clientesService)
        {
            _clientesService = clientesService;
        }

        [HttpGet("")]
        //[HttpGet("GetAllClientes")]
        public async Task<IActionResult> GetAllClientes()
        {
            var clientes = await _clientesService.GetAllClientes();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        //[HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clientesService.GetByIdAsync(id);
            if (cliente == null) return NotFound();

            return Ok(cliente);
        }

        [HttpPost("")]
        //[HttpPost("CreateCliente")]
        public async Task<IActionResult> Create([FromBody] ClienteDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cliente = await _clientesService.CreateClienteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _clientesService.UpdateAsync(id, request);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _clientesService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
