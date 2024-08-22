using DesafioSenaiCimatec.Data;
using DesafioSenaiCimatec.Models;

namespace DesafioSenaiCimatec.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext bancoContext1;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;

        }

        public TB_USUARIO BuscarPorLogin(string email)
        {
            return bancoContext1.TB_USUARIO.FirstOrDefault(x => x.DS_EMAIL.ToUpper() == email);
        }
        public TB_USUARIO ListarPorId(int id)
        {
            return bancoContext1.TB_USUARIO.FirstOrDefault(x => x.ID_USUARIO == id);
        }
        public List<TB_USUARIO> BuscarTodos()
        {
            return bancoContext1.TB_USUARIO.Where(x => !x.StatusExc).ToList();
        }

        public List<TB_USUARIO> ListarTodos()
        {
            return bancoContext1.TB_USUARIO.ToList();
        }


        public TB_USUARIO Adicionar(TB_USUARIO contato)
        {
            bancoContext1.TB_USUARIO.Add(contato);
            bancoContext1.SaveChanges();
            return contato;
        }

        public TB_USUARIO Atualizar(TB_USUARIO contato)
        {
            TB_USUARIO contatoDb = ListarPorId(contato.ID_USUARIO);
            if (contatoDb == null) throw new System.Exception("Houve um erro na atualizacão dos dados do usuário!");
            contatoDb.NM_PESSOA = contato.NM_PESSOA;
            contatoDb.DS_SENHA = contato.DS_SENHA;
            contatoDb.DS_EMAIL = contato.DS_EMAIL;
            contatoDb.DT_NAS_PESSOA = contato.DT_NAS_PESSOA;
            contatoDb.NR_CPF_PES = contato.NR_CPF_PES;
            contatoDb.TP_USUARIO = contato.TP_USUARIO;

            bancoContext1.TB_USUARIO.Update(contatoDb);
            bancoContext1.SaveChanges();
            return contatoDb;
        }

        public bool Apagar(int id)
        {

            TB_USUARIO contatoDb = ListarPorId(id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na exclusão dos dados do usuário!");
            contatoDb.StatusExc = true;


            bancoContext1.TB_USUARIO.Update(contatoDb);
            bancoContext1.SaveChanges();

            return true;

        }

        public TB_USUARIO BuscarPorEmailAlterarSenha(string email, string novaSenha)
        {
            var query = bancoContext1.TB_USUARIO.FirstOrDefault(x => x.DS_EMAIL.ToUpper() == email.ToUpper());
            if (query != null)
            {
                query.DS_SENHA = novaSenha;

                bancoContext1.TB_USUARIO.Update(query);
                bancoContext1.SaveChanges();
            }

            return query;
        }

        public bool ReativarUsuario(int id)
        {

            TB_USUARIO contatoDb = ListarPorId(id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na reativação do usuário!");
            contatoDb.StatusExc = false;


            bancoContext1.TB_USUARIO.Update(contatoDb);
            bancoContext1.SaveChanges();

            return true;

        }
    }
}