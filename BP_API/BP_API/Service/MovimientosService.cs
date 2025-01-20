using BP_API.Contracts;
using BP_API.DTO;
using BP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BP_API.Service
{
    public class MovimientosService : IMovimientos
    {
        private readonly BPContext _bPContext;
        private readonly IParametro _parametro;

        public MovimientosService(BPContext bPContext, IParametro parametro)
        {
            _bPContext = bPContext;
            _parametro = parametro;
        }

        public async Task<List<MovimientoDTO>> GetAllMovimientosAsync()
        {
            var movimientos = await (from m in _bPContext.Movimientos
                                     join c in _bPContext.Cuenta on m.IdCuenta equals c.IdCuenta
                                     join cli in _bPContext.Clientes on c.IdCliente equals cli.IdCliente
                                     join p in _bPContext.Personas on cli.PersonaId equals p.Id
                                     select new MovimientoDTO
                                     {
                                         IdMovimiento = m.IdMovimiento,
                                         IdCuenta = m.IdCuenta,
                                         Fecha = m.Fecha,
                                         TipoMovimiento = m.TipoMovimiento,                                      
                                         Valor = m.Valor,
                                         Saldo = m.Saldo,
                                         ClienteNombre = p.Nombre
                                     }).ToListAsync();

            return movimientos.Select(m => new MovimientoDTO
            {
                IdMovimiento = m.IdMovimiento,
                IdCuenta = m.IdCuenta,
                Fecha = m.Fecha,
                TipoMovimiento = m.TipoMovimiento,
                TipoMovimientoDescripcion = _parametro.GetDescripcionParametrosById(m.TipoMovimiento),
                Valor = m.Valor,
                Saldo = m.Saldo,
                ClienteNombre = m.ClienteNombre
            }).ToList();
        }


        public async Task<MovimientoDTO?> GetByIdAsync(int id)
        {
            var movimiento = await (from m in _bPContext.Movimientos
                                    join c in _bPContext.Cuenta on m.IdCuenta equals c.IdCuenta
                                    join cli in _bPContext.Clientes on c.IdCliente equals cli.IdCliente
                                    join p in _bPContext.Personas on cli.PersonaId equals p.Id
                                    where m.IdMovimiento == id
                                    select new MovimientoDTO
                                    {
                                        IdMovimiento = m.IdMovimiento,
                                        IdCuenta = m.IdCuenta,
                                        Fecha = m.Fecha,
                                        TipoMovimiento = m.TipoMovimiento,
                                        TipoMovimientoDescripcion = _parametro.GetDescripcionParametrosById(m.TipoMovimiento),
                                        Valor = m.Valor,
                                        Saldo = m.Saldo,
                                        ClienteNombre = p.Nombre
                                    }).FirstOrDefaultAsync();

            return movimiento;
        }

        public async Task<MovimientoDTO> CreateMovimientoAsync(MovimientoDTO request)
        {
            var cuenta = await _bPContext.Cuenta.FirstOrDefaultAsync(c => c.IdCuenta == request.IdCuenta);
            if (cuenta == null) throw new Exception("La cuenta no existe.");

            if (request.TipoMovimiento == 15)
            {
                
                if (request.Valor <= 0)
                {
                    throw new Exception("El valor del crédito debe ser mayor que cero.");
                }
            }
            if (request.TipoMovimiento == 16)
            {
                if (cuenta.SaldoInicial <= 0)
                {
                    throw new Exception("No se pueden realizar más débitos, el saldo de la cuenta ha llegado a cero.");
                }

                if (cuenta.SaldoInicial < request.Valor)
                {
                    throw new Exception("Saldo insuficiente para realizar el débito.");
                }
            }

            var nuevoSaldo = request.TipoMovimiento == 15 // Crédito
                ? cuenta.SaldoInicial + request.Valor
                : cuenta.SaldoInicial - request.Valor; // Débito

            if (nuevoSaldo < 0 && request.TipoMovimiento == 16)
            {
                throw new Exception("No se puede realizar el débito, el saldo resultante sería negativo.");
            }

            var movimiento = new Movimiento
            {
                IdCuenta = request.IdCuenta,
                Fecha = DateTime.Now,
                TipoMovimiento = request.TipoMovimiento,
                Valor = request.Valor,
                Saldo = nuevoSaldo,
                FechaIngreso = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            cuenta.SaldoInicial = nuevoSaldo;

            _bPContext.Movimientos.Add(movimiento);
            await _bPContext.SaveChangesAsync();

            return new MovimientoDTO
            {
                IdMovimiento = movimiento.IdMovimiento,
                IdCuenta = movimiento.IdCuenta,
                Fecha = movimiento.Fecha,
                TipoMovimiento = movimiento.TipoMovimiento,
                TipoMovimientoDescripcion = _parametro.GetDescripcionParametrosById(movimiento.TipoMovimiento),
                Valor = movimiento.Valor,
                Saldo = movimiento.Saldo
            };
        }


        public async Task<bool> UpdateMovimientoAsync(int id, MovimientoDTO request)
        {
            var movimiento = await _bPContext.Movimientos.FirstOrDefaultAsync(m => m.IdMovimiento == id);
            if (movimiento == null) return false;

            movimiento.TipoMovimiento = request.TipoMovimiento;
            movimiento.Valor = request.Valor;
            movimiento.Saldo = request.Saldo;
            movimiento.FechaActualizacion = DateTime.Now;

            await _bPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMovimientoAsync(int id)
        {
            var movimiento = await _bPContext.Movimientos.FirstOrDefaultAsync(m => m.IdMovimiento == id);
            if (movimiento == null) return false;

            _bPContext.Movimientos.Remove(movimiento);
            await _bPContext.SaveChangesAsync();
            return true;
        }
    }
}
