using Microsoft.AspNetCore.Mvc;
using DesafioSenaiCimatec.Models;
using System;
using DesafioSenaiCimatec.Repositorio;
using DesafioSenaiCimatec.Helper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DesafioSenaiCimatec.Controllers
{
    public class SugestaoController : Controller

    {
        private readonly ISugestaoRepositorio _sugestaoRepositoriocs;
        private readonly ISessao _sessao;
        public SugestaoController(ISugestaoRepositorio sugestaoRepositoriocs, ISessao sessao)
        {
            _sugestaoRepositoriocs = sugestaoRepositoriocs;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            @ViewBag.NM_PESSOA = usuario.NM_PESSOA;
            @ViewBag.TP_USUARIO = usuario.TP_USUARIO;
            @ViewBag.DS_EMAIL = usuario.DS_EMAIL;
            @ViewBag.NR_CPF_PES = usuario.NR_CPF_PES;
            @ViewBag.DT_NAS_PESSOA = usuario.DT_NAS_PESSOA.ToString("dd/MM/yyyy");

            List<SugestaoModel> sugestao = _sugestaoRepositoriocs.BuscarTodos();
            return View(sugestao);
        }

        [HttpPost]
        public IActionResult Criar(string Descricao)
        {
            string error = string.Empty;
            bool is_action = false;

            var usuario = _sessao.BuscarSessaoDoUsuario();

            try
            {
                if (Descricao == null) throw new Exception("Sugestão não pode ser vazia, por favor digite a sugestão e tente novamente!");

                if (ModelState.IsValid)
                {
                    SugestaoModel sugestao = new SugestaoModel();
                    sugestao.Descricao = Descricao;
                    sugestao.DataSugestao = DateTime.Now;
                    sugestao.UsuarioSugestao = usuario.NM_PESSOA;

                    _sugestaoRepositoriocs.Adicionar(sugestao);

                    is_action = true;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }
    }
}
