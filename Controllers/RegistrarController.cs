using Microsoft.AspNetCore.Mvc;
using DesafioSenaiCimatec.Models;
using DesafioSenaiCimatec.Repositorio;
using DesafioSenaiCimatec.Helper;
using DesafioSenaiCimatec.Util;
using DesafioSenaiCimatec.Filters;

namespace DesafioSenaiCimatec.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class RegistrarController : Controller

    {
        private readonly IUsuarioRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        public RegistrarController(IUsuarioRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult PaginaAdm()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {
                @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
                @ViewBag.TP_USUARIO = usuario.TP_USUARIO;
                @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
                @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
                @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA;
                List<TB_USUARIO> contato = _contatoRepositorio.BuscarTodos();
                return View(contato);
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public virtual IActionResult UsuariosInativosPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch,
                                                                string email, string nome)
        {
            IEnumerable<TB_USUARIO> query = _contatoRepositorio.ListarTodos().Where(x => x.StatusExc == true);

            if (!string.IsNullOrEmpty(email)) query = query.Where(x => x.DS_EMAIL == email);
            if (!string.IsNullOrEmpty(nome)) query = query.Where(x => x.NM_PESSOA == nome);

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.NM_PESSOA.ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();



            int recordsTotal = query.Count();

            List<TB_USUARIO> aList = query.OrderBy(x => x.NM_PESSOA).Skip(iDisplayStart).Take(iDisplayLength).ToList();

            var data = aList.Select(x => new

            {
                ide_usuario = x.ID_USUARIO,
                nome = x.NM_PESSOA,
                email = x.DS_EMAIL,
                cpf = x.NR_CPF_PES,
                dtc_nascimento = x.DT_NAS_PESSOA.ToString("dd/MM/yyyy"),
                perfil = x.TP_USUARIO == Enums.TP_USUARIO.Administrador ? "Admininistrador" : x.TP_USUARIO == Enums.TP_USUARIO.Usuariocadastro ? "Usuario cadastro" : "Usuario consulta",
                reativar = $"<a href='#' type='button' class='btn btn-success' onclick='modalReativar({x.ID_USUARIO})'>Reativar</a>",
            }).ToArray();

            return Json(new
            {
                iDraw = 1,
                sEcho,
                iTotalRecords = recordsTotal,
                iTotalDisplayRecords = recordsTotal,
                aaData = data
            });

        }

        public IActionResult UsuariosInativos()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {
                @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
                @ViewBag.TP_USUARIO = usuario.TP_USUARIO;
                @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
                @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
                @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA;
                List<TB_USUARIO> contato = _contatoRepositorio.BuscarTodos();
            }
            return View();
        }

        public IActionResult Criar()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
            @ViewBag.TP_USUARIO = usuario.TP_USUARIO;
            @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
            @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
            @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA;

            return View();
        }

        [HttpPost]
        public IActionResult Criar(TB_USUARIO contato)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
            @ViewBag.TP_USUARIO = usuario.TP_USUARIO;
            @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
            @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
            @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA;

            try
            {
                if (contato.NM_PESSOA == null || contato.DS_EMAIL == null || contato.NR_CPF_PES == null)
                {
                    TempData["MensagemErro"] = "Campos com (*) são obrigatórios, preencha e tente novamente.";
                }
                
                else
                {
                    string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
                    string senhaNaoEncrypitada = novaSenha;

                    contato.DS_SENHA = Hash.SHA512(novaSenha);
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = $"Usuário cadastrado com sucesso! A senha provisória de {contato.NM_PESSOA} é {senhaNaoEncrypitada}";
                    return RedirectToAction("PaginaAdm", "Registrar");
                }
                return View(contato);

            }
            catch (SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel cadastrar o seu usuário,tente novamente!, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult ReativarUsuario(int ide_usuario)
        {

            string error = string.Empty;
            bool is_action = false;

            try
            {
                _contatoRepositorio.ReativarUsuario(ide_usuario);
                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }
    }
}

