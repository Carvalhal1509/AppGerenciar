using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSenaiCimatec.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o Email")]
        public string DS_EMAIL { get; set; }
        [Required(ErrorMessage = "Digite a Senha")]
        public string DS_SENHA { get; set; }
    }
}