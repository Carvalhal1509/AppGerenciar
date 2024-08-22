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

        public DbSet<TB_USUARIO> TB_USUARIO { get; set; }

        public DbSet<SugestaoModel> Sugestao { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_USUARIO>().ToTable("TB_USUARIO", "dbo");
            base.OnModelCreating(modelBuilder);
        }

    }
}
