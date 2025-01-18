using System;
using System.Collections.Generic;

namespace BP_API.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuentum>();
        }

        public int IdCliente { get; set; }
        public int PersonaId { get; set; }
        public string Contrasena { get; set; } = null!;
        public int Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual Parametro EstadoNavigation { get; set; } = null!;
        public virtual Persona Persona { get; set; } = null!;
        public virtual ICollection<Cuentum> Cuenta { get; set; }
    }
}
