using DesafioSenaiCimatec.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using DesafioSenaiCimatec.Repositorio;
using DesafioSenaiCimatec.Helper;
using System.Collections.Generic;
using DesafioSenaiCimatec.Util;

namespace DesafioSenaiCimatec.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()

        {//se o usuario estiver logado,redirecionar para a home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }
        public IActionResult Sair()
        {
            _sessao.RemoveSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                TB_USUARIO contato = _usuarioRepositorio.BuscarPorLogin(loginModel.DS_EMAIL);

                if (ModelState.IsValid)
                {
                    
                    if (contato.StatusExc == false)
                    {
                        loginModel.DS_SENHA = Hash.SHA512(loginModel.DS_SENHA);

                        if (contato.SenhaValida(loginModel.DS_SENHA))
                        {
                            _sessao.CriarSessaoDoUsuario(contato);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Email ou Senha inválidos. Por favor,tente novamente!.";
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Este usuário não tem mais acesso à esse sistema, Por favor, contate algum Administrador!!";
                    }

                }
                return View("index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel realizar o Login,tente novamente!.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
                    string novaSenhacriptografada = Util.Hash.SHA512(novaSenha);

                    TB_USUARIO contato = _usuarioRepositorio.BuscarPorEmailAlterarSenha(redefinirSenhaModel.DS_EMAIL, novaSenhacriptografada);
                    if (contato != null)
                    {
                        string mensagem = $"<strong>Olá,</strong><br>" +
                            $"<br>" +
                            $"A senha associada ao seu email no nosso sistema foi alterada.<br>" +
                            $"<br>" +
                            $"Sua nova senha é:<br>" +
                            $"<br>" +
                            $"{novaSenha}<br>" +
                            $"<br>" +
                            $"<p>Obrigado por usar o nosso sistema";
                        bool emailEnviado = _email.Enviar(contato.DS_EMAIL, "Sistema Desafio Senai - Nova Senha", mensagem);
                        if (emailEnviado)
                        {
                            TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {

                            TempData["MensagemErro"] = $"Não conseguimos enviar para o seu email cadastrado uma nova senha.Porfavor,tente novamente.";
                            return RedirectToAction("Index", "Login");
                        }
                    }
                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha.Por favor,verifique o email informado.";
                }
                return View("index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel redefinir sua senha! Tente novamente.";
                return RedirectToAction("Index");
            }
        }
    }
}

