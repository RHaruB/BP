using BP_API.DTO;

namespace BP_API.Contracts
{
    public interface IClientes
    {
        Task<List<ClienteDTO>> GetAllClientes();
        Task<ClienteDTO?> GetByIdAsync(int id);
        Task<ClienteDTO> CreateClienteAsync(ClienteDTO request);
        Task<bool> UpdateAsync(int id, ClienteDTO request);
        Task<bool> DeleteAsync(int id);
    }
}