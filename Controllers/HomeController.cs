using DesafioSenaiCimatec.Data;
using DesafioSenaiCimatec.Filters;
using DesafioSenaiCimatec.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using DesafioSenaiCimatec.Util;
using DesafioSenaiCimatec.Repositorio;
using DesafioSenaiCimatec.Models;

namespace DesafioSenaiCimatec.Controllers
{
    [PaginaParaUsuarioLogado]
    public class HomeController : Controller
    {
        private readonly IUsuarioRepositorio _contatoRepositorio;
        private BancoContext _context;
        private readonly ISessao _sessao;

        public HomeController(BancoContext context, ISessao sessao, IUsuarioRepositorio contatoRepositorio)
        {
            _context = context;
            _sessao = sessao;
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {
                @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
                @ViewBag.Perfil = usuario.Perfil;
                @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
                @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
                @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA.ToString("dd/MM/yyyy");

                return View();
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult AlterarSenha(string SenhaAtual, string NovaSenha, string ConfirmarSenha)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
            @ViewBag.Perfil = usuario.Perfil;
            @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
            @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
            @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA;

            try
            {
                if (usuario.DS_SENHA == Hash.SHA512(SenhaAtual))
                {
                    if (NovaSenha == ConfirmarSenha)
                    {
                        NovaSenha = Hash.SHA512(NovaSenha);

                        usuario.DS_SENHA = NovaSenha;
                        _contatoRepositorio.Atualizar(usuario);
                        return Ok(new Response<string>("", "Senha alterada com sucesso!", true));
                    }
                    else
                    {
                        return BadRequest(new Response<string>("", "Nova senha e confirmar nova senha estão diferentes,favor verificar!", false));
                    }
                }
                else
                {
                    return BadRequest(new Response<string>("", "Senha atual invalida, tente novamente!", false));
                }
            }
            catch (SystemException erro)
            {
                return BadRequest(new Response<string>("", "Ops, Não foi possivel mdificar a sua senha ,tente novamente!", false));
            }
        }


        public IActionResult teste()
        {
            return View();
        }


        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult ApagarConfirmacao()
        {
            return View();
        }

        public IActionResult Apagar()
        {
            return View();
        }
    }
}
