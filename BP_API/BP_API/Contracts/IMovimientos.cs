using BP_API.DTO;

namespace BP_API.Contracts
{
    public interface IMovimientos
    {
        Task<List<MovimientoDTO>> GetAllMovimientosAsync();
        Task<MovimientoDTO?> GetByIdAsync(int id);
        Task<MovimientoDTO> CreateMovimientoAsync(MovimientoDTO request);
        Task<bool> UpdateMovimientoAsync(int id, MovimientoDTO request);
        Task<bool> DeleteMovimientoAsync(int id);
    }
}