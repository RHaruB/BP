using BP_API.Contracts;
using BP_API.DTO;
using BP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BP_API.Service
{
    public class ClientesService : IClientes
    {
        BPContext _bPContext;
        private readonly IParametro _parametro;

        public ClientesService(BPContext bPContext, IParametro parametro)
        {
            this._bPContext = bPContext;
            _parametro = parametro;

        }
        public async Task<List<ClienteDTO>> GetAllClientes()
        {
          

            var clientes = await _bPContext.Clientes
            .Include(c => c.Persona) 
            .ToListAsync();

            return clientes.Select(c => new ClienteDTO
            {
                IdCliente = c.IdCliente,
                Id = c.PersonaId,
                Nombre = c.Persona.Nombre,
                Genero = c.Persona.Genero,
                GeneroDescripcion = _parametro.GetDescripcionParametrosById(c.Persona.Genero),
                FechaNacimiento = c.Persona.FechaNacimiento,
                Identificacion = c.Persona.Identificacion,
                TipoIdentificacion =  c.Persona.TipoIdentificacion,
                TipoIdentificacionDescripcion = _parametro.GetDescripcionParametrosById( c.Persona.TipoIdentificacion),
                Direccion = c.Persona.Direccion,
                Telefono = c.Persona.Telefono,
                Contrasena = c.Contrasena,
                Estado = c.Estado,
                EstadoDescripcion = _parametro.GetDescripcionParametrosById(c.Estado),
                Edad = CalcularEdad(c.Persona.FechaNacimiento)
            }).ToList();
        }

        public async Task<ClienteDTO?> GetByIdAsync(int id)
        {
            var cliente = await _bPContext.Clientes
                .Include(c => c.Persona) 
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null) return null;

            return new ClienteDTO
            {
                IdCliente = cliente.IdCliente,
                Id = cliente.PersonaId,
                Nombre = cliente.Persona.Nombre,
                Genero = cliente.Persona.Genero,
                GeneroDescripcion =  _parametro.GetDescripcionParametrosById(cliente.Persona.Genero),
                FechaNacimiento = cliente.Persona.FechaNacimiento,
                Identificacion = cliente.Persona.Identificacion,
                TipoIdentificacion = cliente.Persona.TipoIdentificacion,
                TipoIdentificacionDescripcion = _parametro.GetDescripcionParametrosById(cliente.Persona.TipoIdentificacion),
                Direccion = cliente.Persona.Direccion,
                Telefono = cliente.Persona.Telefono,
                Contrasena = cliente.Contrasena,
                Estado = cliente.Estado,
                EstadoDescripcion = _parametro.GetDescripcionParametrosById(cliente.Estado),
                Edad = CalcularEdad(cliente.Persona.FechaNacimiento)
            };
        }

        public async Task<ClienteDTO> CreateClienteAsync(ClienteDTO request)
        {
            using (var transaction = await _bPContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var persona = new Persona
                    {
                        Nombre = request.Nombre,
                        Genero = request.Genero,
                        FechaNacimiento = request.FechaNacimiento,
                        Identificacion = request.Identificacion,
                        TipoIdentificacion = request.TipoIdentificacion,
                        Direccion = request.Direccion,
                        Telefono = request.Telefono,
                        FechaIngreso = DateTime.Now,
                        FechaActualizacion = DateTime.Now
                    };
                    _bPContext.Personas.Add(persona);
                    await _bPContext.SaveChangesAsync();

                    var cliente = new Cliente
                    {
                        PersonaId = persona.Id, 
                        Contrasena = request.Contrasena,
                        Estado = request.Estado,
                        FechaIngreso = DateTime.Now,
                        FechaActualizacion = DateTime.Now
                    };

                    _bPContext.Clientes.Add(cliente);
                    await _bPContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new ClienteDTO
                    {
                        IdCliente = cliente.IdCliente,
                        Id = cliente.PersonaId,
                        Nombre = persona.Nombre,
                        Contrasena = cliente.Contrasena,
                        Estado = cliente.Estado
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<bool> UpdateAsync(int id, ClienteDTO request)
        {
            var cliente = await _bPContext.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id);
            if (cliente == null) return false;

            cliente.PersonaId = request.Id;
            cliente.Contrasena = request.Contrasena;
            cliente.Estado = request.Estado;
            cliente.FechaActualizacion = DateTime.Now;

            await _bPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var estados = _parametro.GetParametros(3);
            var estadoEliminado = estados.Where(s => s.Valor == "Inactivo").FirstOrDefault().IdParametro; 

            var cliente = await _bPContext.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id);
            if (cliente == null) return false;
            
            cliente.Estado = estadoEliminado;
            cliente.FechaActualizacion = DateTime.Now;

            await _bPContext.SaveChangesAsync();
            return true;
        }

        private int CalcularEdad(DateTime FechaNacimiento)
        {
            DateTime hoy = DateTime.Now;
            int edad = hoy.Year - FechaNacimiento.Year;
            if (FechaNacimiento.Date > hoy.AddYears(-edad))
            {
                edad--;
            }
            return edad;
        }
    }
}