using BP_API.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata;

using System.Text;
using BP_API.Models;
using Microsoft.EntityFrameworkCore;
using BP_API.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;

namespace BP_API.Service
{
    public class ReporteService : IReporte
    {
        private readonly BPContext _bPContext;
        private readonly IParametro _parametro;

        public ReporteService(BPContext bPContext, IParametro parametro)
        {
            this._bPContext = bPContext;
            _parametro = parametro;

        }

        public async Task<ReporteEstadoCuentaDTO> GenerarEstadoCuentaAsync(ReporteEstadoCuentaParametrosDTO parametros)
        {
            // Obtener datos del cliente
            var cliente = await (from cli in _bPContext.Clientes
                                 join p in _bPContext.Personas on cli.PersonaId equals p.Id
                                 where cli.IdCliente == parametros.IdCliente
                                 select new
                                 {
                                     cli.IdCliente,
                                     p.Nombre
                                 }).FirstOrDefaultAsync();

            if (cliente == null)
            {
                throw new Exception("El cliente especificado no existe.");
            }

            // Obtener las cuentas del cliente
            var cuentas = await (from c in _bPContext.Cuenta
                                 where c.IdCliente == parametros.IdCliente
                                 select new CuentaDetalleDTO
                                 {
                                     IdCuenta = c.IdCuenta,
                                     NumeroCuenta = c.NumeroCuenta,
                                     Saldo = c.SaldoInicial
                                 }).ToListAsync();

            // Agregar movimientos a las cuentas
            decimal totalCreditos = 0;
            decimal totalDebitos = 0;

            foreach (var cuenta in cuentas)
            {
                var movimientos = await (from m in _bPContext.Movimientos
                                         where m.IdCuenta == cuenta.IdCuenta &&
                                               m.Fecha >= parametros.FechaInicio &&
                                               m.Fecha <= parametros.FechaFin
                                         select new MovimientoDTO
                                         {
                                             IdMovimiento = m.IdMovimiento,
                                             IdCuenta = m.IdCuenta,
                                             Fecha = m.Fecha,
                                             TipoMovimiento = m.TipoMovimiento,

                                             Valor = m.Valor,
                                             Saldo = m.Saldo
                                         }).ToListAsync();
                movimientos.Select(s => s.TipoMovimientoDescripcion = _parametro.GetDescripcionParametrosById(s.TipoMovimiento));

                cuenta.Movimientos = movimientos;

                // Calcular totales
                totalCreditos += movimientos
                    .Where(m => m.TipoMovimiento == 15) // Suponiendo que 15 es Crédito
                    .Sum(m => m.Valor);

                totalDebitos += movimientos
                    .Where(m => m.TipoMovimiento == 16) // Suponiendo que 16 es Débito
                    .Sum(m => m.Valor);
            }

            return new ReporteEstadoCuentaDTO
            {
                ClienteNombre = cliente.Nombre,
                Cuentas = cuentas,
                TotalCreditos = totalCreditos,
                TotalDebitos = totalDebitos
            };
        }

        public async Task<string> GenerarPdfEstadoCuentaAsync(ReporteEstadoCuentaDTO reporte)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Crear el documento PDF
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);

                        // Contenido de la página
                        page.Content().Column(col =>
                        {
                            // Título del reporte
                            col.Item().Text("Estado de Cuenta")
                                .Bold().FontSize(18).AlignCenter();

                            // Información del cliente
                            col.Item().Text($"Cliente: {reporte.ClienteNombre}")
                                .FontSize(12);
                            col.Item().Text($"Total Créditos: {reporte.TotalCreditos:C2}");
                            col.Item().Text($"Total Débitos: {reporte.TotalDebitos:C2}");

                            col.Item().Text("\n");

                            // Iterar sobre las cuentas asociadas
                            foreach (var cuenta in reporte.Cuentas)
                            {
                                // Información de la cuenta
                                col.Item().Text($"Cuenta: {cuenta.NumeroCuenta} - Saldo: {cuenta.Saldo:C2}")
                                    .FontSize(12);

                                // Crear tabla para movimientos
                                col.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(100);
                                        columns.RelativeColumn(200);
                                        columns.RelativeColumn(200);
                                        columns.RelativeColumn(100);
                                    });

                                    // Cabecera de la tabla
                                    table.Cell().Text("ID Movimiento");
                                    table.Cell().Text("Fecha");
                                    table.Cell().Text("Tipo Movimiento");
                                    table.Cell().Text("Valor");

                                    // Llenar la tabla con movimientos
                                    foreach (var movimiento in cuenta.Movimientos)
                                    {
                                        string fechaString = movimiento.Fecha.HasValue
                                                     ? movimiento.Fecha.Value.ToString("yyyy-MM-dd")
                                                      : "Fecha no disponible";
                                        table.Cell().Text(movimiento.IdMovimiento.ToString());
                                        table.Cell().Text(fechaString);
                                        table.Cell().Text(movimiento.TipoMovimientoDescripcion);
                                        table.Cell().Text(movimiento.Valor.ToString("C2"));
                                    }
                                });

                                col.Item().Text("\n");
                            }
                        });
                    });
                })
                .GeneratePdf(memoryStream);

                // Convertir el contenido a base64
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }


    }
}
