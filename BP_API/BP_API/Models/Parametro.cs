using System;
using System.Collections.Generic;

namespace BP_API.Models
{
    public partial class Parametro
    {
        public Parametro()
        {
            Clientes = new HashSet<Cliente>();
            CuentumEstadoNavigations = new HashSet<Cuentum>();
            CuentumTipoNavigations = new HashSet<Cuentum>();
            Movimientos = new HashSet<Movimiento>();
            PersonaGeneroNavigations = new HashSet<Persona>();
            PersonaTipoIdentificacionNavigations = new HashSet<Persona>();
        }

        public int IdParametro { get; set; }
        public int Tipo { get; set; }
        public string? Clave { get; set; }
        public string? Valor { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Cuentum> CuentumEstadoNavigations { get; set; }
        public virtual ICollection<Cuentum> CuentumTipoNavigations { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
        public virtual ICollection<Persona> PersonaGeneroNavigations { get; set; }
        public virtual ICollection<Persona> PersonaTipoIdentificacionNavigations { get; set; }
    }
}
