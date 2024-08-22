using DesafioSenaiCimatec.Models;
using Newtonsoft.Json;

namespace DesafioSenaiCimatec.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public TB_USUARIO BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            return JsonConvert.DeserializeObject<TB_USUARIO>(sessaoUsuario);
        }

        public void CriarSessaoDoUsuario(TB_USUARIO contato)
        {
            string valor = JsonConvert.SerializeObject(contato);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public void RemoveSessaoDoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}

