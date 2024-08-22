using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;




namespace DesafioSenaiCimatec.Models
{
    public class SugestaoModel
    {

        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataSugestao { get; set; }
        public string UsuarioSugestao { get; set; }
        public bool StatusExc { get; set; }

    }
}