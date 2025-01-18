using BP_API.Contracts;
using BP_API.DTO;
using BP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BP_API.Service
{
    public class CuentaService: ICuenta
    {
        private readonly BPContext _bPContext;
        private readonly IParametro _parametro;

        public CuentaService(BPContext bPContext, IParametro parametro)
        {
            this._bPContext = bPContext;
            this._parametro = parametro;
        }

        public async Task<List<CuentaDTO>> GetAllCuentasAsync()
        {
            var cuentas = await (from c in _bPContext.Cuenta
                                 join cliente in _bPContext.Clientes on c.IdCliente equals cliente.IdCliente
                                 join persona in _bPContext.Personas on cliente.PersonaId equals persona.Id
                                 select new
                                 {
                                     c.IdCuenta,
                                     c.IdCliente,
                                     c.NumeroCuenta,
                                     c.Tipo,
                                     c.SaldoInicial,
                                     c.Estado,
                                     ClienteNombre = persona.Nombre
                                 }).ToListAsync();

            return cuentas.Select(c => new CuentaDTO
            {
                IdCuenta = c.IdCuenta,
                IdCliente = c.IdCliente,
                NumeroCuenta = c.NumeroCuenta,
                Tipo = c.Tipo,
                TipoDescripcion = _parametro.GetDescripcionParametrosById(c.Tipo),
                SaldoInicial = c.SaldoInicial,
                Estado = c.Estado,
                EstadoDescripcion = _parametro.GetDescripcionParametrosById(c.Estado),
                ClienteNombre = c.ClienteNombre
            }).ToList();
        }

        public async Task<CuentaDTO?> GetByIdAsync(int id)
        {
            var cuenta = await (from c in _bPContext.Cuenta
                                join cliente in _bPContext.Clientes on c.IdCliente equals cliente.IdCliente
                                join persona in _bPContext.Personas on cliente.PersonaId equals persona.Id
                                where c.IdCuenta == id
                                select new
                                {
                                    c.IdCuenta,
                                    c.IdCliente,
                                    c.NumeroCuenta,
                                    c.Tipo,
                                    c.SaldoInicial,
                                    c.Estado,
                                    c.FechaIngreso,
                                    c.FechaActualizacion,
                                    ClienteNombre = persona.Nombre,
                                    PersonaGenero = persona.Genero
                                }).FirstOrDefaultAsync();

            if (cuenta == null) return null;

            return new CuentaDTO
            {
                IdCuenta = cuenta.IdCuenta,
                IdCliente = cuenta.IdCliente,
                NumeroCuenta = cuenta.NumeroCuenta,
                Tipo = cuenta.Tipo,
                TipoDescripcion = _parametro.GetDescripcionParametrosById(cuenta.Tipo),
                SaldoInicial = cuenta.SaldoInicial,
                Estado = cuenta.Estado,
                EstadoDescripcion = _parametro.GetDescripcionParametrosById(cuenta.Estado),
                ClienteNombre = cuenta.ClienteNombre
            };
        }

        public async Task<CuentaDTO> CreateCuentaAsync(CuentaDTO request)
        {
            using (var transaction = await _bPContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var cuenta = new Cuentum
                    {
                        IdCliente = request.IdCliente,
                        NumeroCuenta = request.NumeroCuenta,
                        Tipo = request.Tipo,
                        SaldoInicial = request.SaldoInicial,
                        Estado = request.Estado
                    };

                    _bPContext.Cuenta.Add(cuenta);
                    await _bPContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return new CuentaDTO
                    {
                        IdCuenta = cuenta.IdCuenta,
                        IdCliente = cuenta.IdCliente,
                        NumeroCuenta = cuenta.NumeroCuenta,
                        Tipo = cuenta.Tipo,
                        Estado = cuenta.Estado,
                        SaldoInicial = cuenta.SaldoInicial
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<bool> UpdateCuentaAsync(int id, CuentaDTO request)
        {
            var cuenta = await _bPContext.Cuenta.FirstOrDefaultAsync(c => c.IdCuenta == id);
            if (cuenta == null) return false;

            cuenta.NumeroCuenta = request.NumeroCuenta;
            cuenta.Tipo = request.Tipo;
            cuenta.SaldoInicial = request.SaldoInicial;
            cuenta.Estado = request.Estado;
            cuenta.FechaActualizacion = DateTime.Now;

            await _bPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCuentaAsync(int id)
        {
            var cuenta = await _bPContext.Cuenta.FirstOrDefaultAsync(c => c.IdCuenta == id);
            if (cuenta == null) return false;

            var estadoEliminado = _parametro.GetParametros(3).FirstOrDefault(p => p.Valor == "Inactivo")?.IdParametro;

            if (estadoEliminado == null) throw new Exception("Estado 'Inactivo' no configurado en los parámetros.");

            cuenta.Estado = estadoEliminado.Value;
            cuenta.FechaActualizacion = DateTime.Now;

            await _bPContext.SaveChangesAsync();
            return true;
        }
    }
}
