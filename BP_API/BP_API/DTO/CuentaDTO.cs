namespace BP_API.DTO
{
    public class CuentaDTO
    {
        public int? IdCuenta { get; set; }
        public int IdCliente { get; set; }
        public string? NumeroCuenta { get; set; }
        public int Tipo { get; set; }
        public string? TipoDescripcion { get; set; }
        public decimal SaldoInicial { get; set; }
        public int Estado { get; set; }
        public string? EstadoDescripcion { get; set; }
        public string? ClienteNombre { get; set; }
      
    }
}