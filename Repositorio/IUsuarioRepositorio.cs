using DesafioSenaiCimatec.Models;

namespace DesafioSenaiCimatec.Repositorio
{
    public interface IUsuarioRepositorio
    {
        TB_USUARIO BuscarPorLogin(string email);
        TB_USUARIO BuscarPorEmailAlterarSenha(string email, string novaSenha);
        TB_USUARIO ListarPorId(int id);
        List<TB_USUARIO> BuscarTodos();
        List<TB_USUARIO> ListarTodos();
        TB_USUARIO Adicionar(TB_USUARIO contato);
        TB_USUARIO Atualizar(TB_USUARIO contato);
        bool Apagar(int id);
        bool ReativarUsuario(int id);

    }
}