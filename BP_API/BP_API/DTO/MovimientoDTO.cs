namespace BP_API.DTO
{
    public class MovimientoDTO
    {
        public int IdMovimiento { get; set; }
        public int IdCuenta { get; set; }
        public string? NumeroCuenta { get; set; }
        public DateTime? Fecha { get; set; }
        public int TipoMovimiento { get; set; }
        public string? TipoMovimientoDescripcion { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public decimal? Saldo { get; set; }
        public string? ClienteNombre { get; set; } = string.Empty;
    }
}