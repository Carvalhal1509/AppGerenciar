using DesafioSenaiCimatec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DesafioSenaiCimatec.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(TB_USUARIO contato);
        void RemoveSessaoDoUsuario();
        TB_USUARIO BuscarSessaoDoUsuario();

    }
}