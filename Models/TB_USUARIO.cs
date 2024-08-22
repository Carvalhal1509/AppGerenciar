using DesafioSenaiCimatec.Enums;
using System.ComponentModel.DataAnnotations;

namespace DesafioSenaiCimatec.Models
{
    public class TB_USUARIO
    {
        [Key]
        public int ID_USUARIO { get; set; }

        [Required(ErrorMessage = "Digite o Nome")]
        public string NM_PESSOA { get; set; }

        [Required(ErrorMessage = "Digite a Senha")]
        public string DS_SENHA { get; set; }

        [Required(ErrorMessage = "Digite o Email")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string DS_EMAIL { get; set; }

        [Required(ErrorMessage = "Digite o Cpf")]
        public int NR_CPF_PES { get; set; }

        public DateTime DT_NAS_PESSOA { get; set; }

        [Required(ErrorMessage = "Selecione o Perfil")]
        public TP_USUARIO Perfil { get; set; }

        public bool StatusExc { get; set; }

        public bool SenhaValida(string senha)
        {
            return DS_SENHA == senha;
        }
    }
}
