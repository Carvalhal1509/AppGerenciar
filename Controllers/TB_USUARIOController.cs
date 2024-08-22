using DesafioSenaiCimatec.Data;
using DesafioSenaiCimatec.Models;
using DesafioSenaiCimatec.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DesafioSenaiCimatec.Controllers
{
    public class TB_USUARIOController : Controller
    {
        private BancoContext _context;

        public TB_USUARIOController(BancoContext context)
        {
            _context = context;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(TB_USUARIO Usuario)
        {
            return View();
        }
        [HttpPost]
        public IActionResult SalvarUsuario(TB_USUARIO Usuario, string informacaoEmail2, string informacaoSenha2)
        {

            try
            {


                if (Usuario.NM_PESSOA == null || Usuario.DS_EMAIL == null || Usuario.DS_SENHA == null || Usuario.NR_CPF_PES == informacaoEmail2 == null || informacaoSenha2 == null)
                {
                    return BadRequest(new Response<string>("", "Campos com (*) são obrigatórios, preencha e tente novamente.", false));
                }

                if (Usuario.DS_SENHA != informacaoSenha2)
                {
                    return BadRequest(new Response<string>("", "Senha não são iguais, digite senha igual à confirmação de senha e tente novamente", false));
                }

                if (Usuario.DS_EMAIL != informacaoEmail2)
                {
                    return BadRequest(new Response<string>("", "Email não são iguais, digite email igual à confirmação de email e tente novamente", false));
                }

                Usuario.DS_SENHA = Hash.SHA512(Usuario.DS_SENHA);

                var query = _context.TB_USUARIO.Where(x => x.DS_EMAIL == Usuario.DS_EMAIL).FirstOrDefault();

                Usuario.TP_USUARIO = Enums.TP_USUARIO.Usuariocadastro;
                _context.TB_USUARIO.Add(Usuario);
                _context.SaveChanges();
                return Ok(new Response<string>("", "Salvo com sucesso", true));

            }
            catch (Exception erro)
            {
                return BadRequest(new Response<string>("", "Ocorreu um erro ao salvar o usuário: " + erro.Message, false));
            }
        }
    }
}