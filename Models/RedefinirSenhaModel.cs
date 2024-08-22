using System.ComponentModel.DataAnnotations;

namespace DesafioSenaiCimatec.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o e-mail")]
        public string DS_EMAIL { get; set; }
    }
}