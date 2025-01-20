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
        public async Task<string> GetNumeroCuentasByIdAsync()
        {
            var parametro = _bPContext.Parametros
           .Where(p => p.Tipo == 6 && p.Clave == "cta")
           .FirstOrDefault();
            string numeroCTASTR = string.Empty;

            long numerocta = 0;

            if (parametro != null && parametro.Valor != null)
            {
                numerocta = long.Parse(parametro.Valor);
                numerocta++;

                parametro.Valor = numerocta.ToString();
                numeroCTASTR = numerocta.ToString();
                await _bPContext.SaveChangesAsync();

            }

            return numeroCTASTR;
        }
    }
}