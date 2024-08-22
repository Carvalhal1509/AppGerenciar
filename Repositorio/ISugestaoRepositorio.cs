using DesafioSenaiCimatec.Models;

namespace DesafioSenaiCimatec.Repositorio
{
    public interface ISugestaoRepositorio
    {
        public SugestaoModel ListarPorId(int id);
        public List<SugestaoModel> BuscarTodos();
        public SugestaoModel Adicionar(SugestaoModel sugestao);
    }
}

