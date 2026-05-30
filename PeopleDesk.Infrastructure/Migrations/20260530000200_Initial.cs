using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleDesk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chamados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Prioridade = table.Column<int>(type: "int", nullable: false),
                    UsuarioCriadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioResponsavelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IniciadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObservacaoFechamento = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamados", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_Prioridade",
                table: "Chamados",
                column: "Prioridade");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_Status",
                table: "Chamados",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_UsuarioCriadorId",
                table: "Chamados",
                column: "UsuarioCriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_UsuarioResponsavelId",
                table: "Chamados",
                column: "UsuarioResponsavelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamados");
        }
    }
}
