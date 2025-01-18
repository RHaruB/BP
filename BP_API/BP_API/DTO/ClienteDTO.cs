namespace BP_API.DTO
{
    public class ClienteDTO : PersonaDTO
    {
        public int IdCliente { get; set; }
        public string Contrasena { get; set; }
        public int Estado { get; set; } 
        public string EstadoDescripcion { get; set; }
    }
}