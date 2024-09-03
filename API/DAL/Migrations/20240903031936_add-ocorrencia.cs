using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addocorrencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_Ocorrencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Primeiro_Nome = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Sobrenome = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Assunto = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Mensagem = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    Concordo_Termos_Condicoes = table.Column<string>(type: "NVARCHAR2(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Ocorrencia", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Ocorrencia");
        }
    }
}
