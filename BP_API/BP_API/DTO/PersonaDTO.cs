namespace BP_API.DTO
{
    public class PersonaDTO 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Genero { get; set; }
        public string? GeneroDescripcion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public int TipoIdentificacion { get; set; }
        public string? TipoIdentificacionDescripcion { get; set; }
    }  
}