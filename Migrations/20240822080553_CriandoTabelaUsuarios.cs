using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioSenaiCimatec.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sugestao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSugestao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioSugestao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusExc = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sugestao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NM_PESSOA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DS_SENHA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DS_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NR_CPF_PES = table.Column<int>(type: "int", nullable: false),
                    DT_NAS_PESSOA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Perfil = table.Column<int>(type: "int", nullable: false),
                    StatusExc = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID_USUARIO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sugestao");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
