using DesafioSenaiCimatec.Data;
using DesafioSenaiCimatec.Models;

namespace DesafioSenaiCimatec.Repositorio
{
    public class SugestaoRepositorio : ISugestaoRepositorio
    {
        private readonly BancoContext bancoContext1;
        public SugestaoRepositorio(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;

        }

        public SugestaoModel ListarPorId(int id)
        {
            return bancoContext1.Sugestao.FirstOrDefault(x => x.Id == id);
        }
        public List<SugestaoModel> BuscarTodos()
        {
            return bancoContext1.Sugestao.Where(x => !x.StatusExc).ToList();
        }

        public SugestaoModel Adicionar(SugestaoModel sugestao)
        {
            bancoContext1.Sugestao.Add(sugestao);
            bancoContext1.SaveChanges();
            return sugestao;
        }

    }

}
