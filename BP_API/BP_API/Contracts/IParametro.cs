using BP_API.DTO;

namespace BP_API.Contracts
{
    public interface IParametro
    {
        List<ParametroDTO> GetParametros(int id);
        string GetDescripcionParametrosById(int id);
    }
}
