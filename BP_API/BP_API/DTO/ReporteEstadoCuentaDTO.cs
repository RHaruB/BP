namespace BP_API.DTO
{
    public class ReporteEstadoCuentaDTO
    {

        public string ClienteNombre { get; set; }
        public List<CuentaDetalleDTO> Cuentas { get; set; } = new List<CuentaDetalleDTO>();
        public decimal TotalCreditos { get; set; }
        public decimal TotalDebitos { get; set; }
    }

    public class CuentaDetalleDTO
    {
        public int IdCuenta { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public List<MovimientoDTO> Movimientos { get; set; } = new List<MovimientoDTO>();
    }
    public class ReporteEstadoCuentaParametrosDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdCliente { get; set; }
    }
}
