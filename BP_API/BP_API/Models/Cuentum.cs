using System;
using System.Collections.Generic;

namespace BP_API.Models
{
    public partial class Cuentum
    {
        public Cuentum()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int IdCuenta { get; set; }
        public int IdCliente { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public int Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public int Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual Parametro EstadoNavigation { get; set; } = null!;
        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Parametro TipoNavigation { get; set; } = null!;
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
