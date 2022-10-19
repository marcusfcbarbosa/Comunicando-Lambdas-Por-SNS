using System.Threading.Tasks;

namespace user_changes_domain.Interfaces
{
    public interface ITituloRepository
    {
        Task<string> SalvarTitulo(EnvioTitulo titulo);
    }

    public class TituloRepository : ITituloRepository
    {
        public async Task<string> SalvarTitulo(EnvioTitulo titulo)
        {
            return $"Chegou no repositorio TituloRepository  titulo.idLinha = {titulo.idLinha} titulo.seuNumero = {titulo.seuNumero}";
        }
    }

}
