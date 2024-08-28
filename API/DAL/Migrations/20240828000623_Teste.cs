using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_Cidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Cidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Nuvem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Dia_1 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_2 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_3 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_4 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_5 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_6 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_7 = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Nuvem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Precipitacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Dia_1 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_2 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_3 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_4 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_5 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_6 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_7 = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Precipitacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Temperatura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Dia_1 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_2 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_3 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_4 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_5 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_6 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_7 = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Temperatura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Umidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Dia_1 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_2 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_3 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_4 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_5 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_6 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_7 = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Umidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Ventania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Dia_1 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_2 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_3 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_4 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_5 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_6 = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Dia_7 = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Ventania", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Regiao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Cidade_Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Regiao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Regiao_TBL_Cidade_Cidade_Id",
                        column: x => x.Cidade_Id,
                        principalTable: "TBL_Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Previsao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Regiao_Id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Temperatura_Id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Umidade_Id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Ventania_Id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Precipitacao_Id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Nuvem_Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Previsao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Previsao_TBL_Nuvem_Nuvem_Id",
                        column: x => x.Nuvem_Id,
                        principalTable: "TBL_Nuvem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Previsao_TBL_Precipitacao_Precipitacao_Id",
                        column: x => x.Precipitacao_Id,
                        principalTable: "TBL_Precipitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Previsao_TBL_Regiao_Regiao_Id",
                        column: x => x.Regiao_Id,
                        principalTable: "TBL_Regiao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Previsao_TBL_Temperatura_Temperatura_Id",
                        column: x => x.Temperatura_Id,
                        principalTable: "TBL_Temperatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Previsao_TBL_Umidade_Umidade_Id",
                        column: x => x.Umidade_Id,
                        principalTable: "TBL_Umidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Previsao_TBL_Ventania_Ventania_Id",
                        column: x => x.Ventania_Id,
                        principalTable: "TBL_Ventania",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SubPrefeitura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Regiao_Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SubPrefeitura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_SubPrefeitura_TBL_Regiao_Regiao_Id",
                        column: x => x.Regiao_Id,
                        principalTable: "TBL_Regiao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Aviso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Nivel = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    SubPrefeitura_Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Aviso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Aviso_TBL_SubPrefeitura_SubPrefeitura_Id",
                        column: x => x.SubPrefeitura_Id,
                        principalTable: "TBL_SubPrefeitura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Aviso_SubPrefeitura_Id",
                table: "TBL_Aviso",
                column: "SubPrefeitura_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Previsao_Nuvem_Id",
                table: "TBL_Previsao",
                column: "Nuvem_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Previsao_Precipitacao_Id",
                table: "TBL_Previsao",
                column: "Precipitacao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Previsao_Regiao_Id",
                table: "TBL_Previsao",
                column: "Regiao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Previsao_Temperatura_Id",
                table: "TBL_Previsao",
                column: "Temperatura_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Previsao_Umidade_Id",
                table: "TBL_Previsao",
                column: "Umidade_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Previsao_Ventania_Id",
                table: "TBL_Previsao",
                column: "Ventania_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Regiao_Cidade_Id",
                table: "TBL_Regiao",
                column: "Cidade_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SubPrefeitura_Regiao_Id",
                table: "TBL_SubPrefeitura",
                column: "Regiao_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Aviso");

            migrationBuilder.DropTable(
                name: "TBL_Previsao");

            migrationBuilder.DropTable(
                name: "TBL_SubPrefeitura");

            migrationBuilder.DropTable(
                name: "TBL_Nuvem");

            migrationBuilder.DropTable(
                name: "TBL_Precipitacao");

            migrationBuilder.DropTable(
                name: "TBL_Temperatura");

            migrationBuilder.DropTable(
                name: "TBL_Umidade");

            migrationBuilder.DropTable(
                name: "TBL_Ventania");

            migrationBuilder.DropTable(
                name: "TBL_Regiao");

            migrationBuilder.DropTable(
                name: "TBL_Cidade");
        }
    }
}
