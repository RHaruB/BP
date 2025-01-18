using BP_API.DTO;

namespace BP_API.Contracts
{
    public interface ICuenta
    {
         Task<List<CuentaDTO>> GetAllCuentasAsync();
        Task<CuentaDTO?> GetByIdAsync(int id);
        Task<CuentaDTO> CreateCuentaAsync(CuentaDTO request);
        Task<bool> UpdateCuentaAsync(int id, CuentaDTO request);
        Task<bool> DeleteCuentaAsync(int id);

    }
}
