using DesafioSenaiCimatec.Data;
using DesafioSenaiCimatec.Filters;
using DesafioSenaiCimatec.Helper;
using DesafioSenaiCimatec.Models;
using DesafioSenaiCimatec.Repositorio;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DesafioSenaiCimatec.Controllers
{
    [PaginaParaUsuarioLogado]
    public class PessoaController : Controller
    {
        private readonly IUsuarioRepositorio _contatoRepositorio;
        private BancoContext _context;
        private readonly ISessao _sessao;
        public PessoaController(IUsuarioRepositorio contatoRepositorio, BancoContext context, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _context = context;
            _sessao = sessao;
        }

        public IActionResult Eventos()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {
                @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
                @ViewBag.TP_USUARIO = usuario.TP_USUARIO;
                @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
                @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
                @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA;
                return View();
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
            //List<Usuarios> contato = _c,ontatoRepositorio.BuscarTodos();
        }


        public IActionResult Editar(int id)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
            @ViewBag.TP_USUARIO = usuario.TP_USUARIO;
            @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
            @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
            @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA;


            TB_USUARIO contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
            @ViewBag.TP_USUARIO = usuario.TP_USUARIO;
            @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
            @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
            @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA;

            TB_USUARIO contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                _contatoRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = "Usuário deletado com sucesso!";
                return RedirectToAction("PaginaAdm", "Registrar");
            }
            catch
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel deletar o seu usuário, tente novamente!.";
                return RedirectToAction("PaginaAdm", "Registrar");

            }
        }


        [HttpPost]
        public IActionResult Alterar(TB_USUARIO contato)
        {

            var query = _context.Usuarios.Where(x => x.ID_USUARIO == contato.ID_USUARIO).FirstOrDefault();
            var buscaPorEmail = _context.Usuarios.Where(x => x.DS_EMAIL == contato.DS_EMAIL).FirstOrDefault();

            try
            {
                if (query != null && contato.DS_EMAIL == query.DS_EMAIL)
                {
                    contato.DS_SENHA = query.DS_SENHA;
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                    return RedirectToAction("PaginaAdm", "Registrar");
                }
                else if (buscaPorEmail != null)
                {
                    TempData["MensagemErro"] = "Este email já está cadastrado no nosso sistema, troque o email e tente novamente";
                    return RedirectToAction("PaginaAdm", "Registrar");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, Não foi possivel alterar esse usuário, tente novamente!";
                    return View("Editar", contato);
                }
            }
            catch (SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel alterar o seu usuário,tente novamente!, detalhe do erro:{erro.Message}";
                return RedirectToAction("Editar", "Contato");
            }

        }
    }
}
