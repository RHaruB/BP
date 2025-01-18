using BP_API.Contracts;
using BP_API.DTO;
using BP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BP_API.Service
{
    public class ParametroService : IParametro
    {
        BPContext _bPContext;

        public ParametroService(BPContext bPContext)
        {
            this._bPContext = bPContext;
        }

        public List<ParametroDTO> GetParametros(int id)
        {
            List<ParametroDTO> parametros = new List<ParametroDTO>();
            ParametroDTO parametro = new ParametroDTO();

            parametros = _bPContext.Parametros
           .Where(p => p.Tipo == id)
           .Select(p => new ParametroDTO
           {
               IdParametro = p.IdParametro,
               Clave = p.Clave,
               Valor = p.Valor,
               Tipo = p.Tipo
           })
           .ToList();


            return parametros;
        }
        public string GetDescripcionParametrosById(int id)
        {
            var parametro = _bPContext.Parametros
           .Where(p => p.IdParametro == id)
           .FirstOrDefault().Valor;

            return parametro;
        }
    }
}