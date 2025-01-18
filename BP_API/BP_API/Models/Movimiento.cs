using System;
using System.Collections.Generic;

namespace BP_API.Models
{
    public partial class Movimiento
    {
        public int IdMovimiento { get; set; }
        public int IdCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual Cuentum IdCuentaNavigation { get; set; } = null!;
        public virtual Parametro TipoMovimientoNavigation { get; set; } = null!;
    }
}
