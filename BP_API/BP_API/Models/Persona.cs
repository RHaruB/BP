using System;
using System.Collections.Generic;

namespace BP_API.Models
{
    public partial class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Identificacion { get; set; } = null!;
        public int TipoIdentificacion { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual Parametro GeneroNavigation { get; set; } = null!;
        public virtual Parametro TipoIdentificacionNavigation { get; set; } = null!;
        public virtual Cliente? Cliente { get; set; }
    }
}
