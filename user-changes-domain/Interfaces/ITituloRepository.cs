using System.Threading.Tasks;
using user_changes_domain.Data;
using user_changes_domain.Data.Entities;

namespace user_changes_domain.Interfaces
{
    public interface ITituloRepository
    {
        Task<string> SalvarTitulo(EnvioTitulo titulo);
    }

    public class TituloRepository : ITituloRepository
    {
        private readonly FibraContext _fibraContext;
        public TituloRepository(FibraContext fibraContext)
        {
            _fibraContext = fibraContext;
        }
        public async Task<string> SalvarTitulo(EnvioTitulo titulo)
        {
            _fibraContext.Titulos.Add(new Titulo(titulo.idLinha, titulo.codEspecieDoc, titulo.seuNumero, titulo.dataVencimento));
            await _fibraContext.SaveChangesAsync();
            return $"Chegou no repositorio TituloRepository  titulo.idLinha = {titulo.idLinha} titulo.seuNumero = {titulo.seuNumero}";
        }
    }

}
