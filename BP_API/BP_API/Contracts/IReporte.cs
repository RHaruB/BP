using BP_API.DTO;

namespace BP_API.Contracts
{
    public interface IReporte
    {
        Task<ReporteEstadoCuentaDTO> GenerarEstadoCuentaAsync(ReporteEstadoCuentaParametrosDTO parametros);
        Task<string> GenerarPdfEstadoCuentaAsync(ReporteEstadoCuentaDTO reporte);

    }
}
