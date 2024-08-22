using DesafioSenaiCimatec.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace DesafioSenaiCimatec.Data
{
    public class BancoContext : DbContext

    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<TB_USUARIO> Usuarios { get; set; }

        public DbSet<SugestaoModel> Sugestao { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
