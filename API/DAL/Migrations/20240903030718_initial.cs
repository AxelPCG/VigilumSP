using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_Municipio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Cod_Mun = table.Column<int>(type: "NUMBER(10)", maxLength: 7, nullable: false),
                    Nm_Mun = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Sg_Estado = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    Area_Km2 = table.Column<double>(type: "BINARY_DOUBLE", precision: 10, scale: 2, nullable: false),
                    Geometry = table.Column<string>(type: "SDO_GEOMETRY", nullable: false),
                    Cord_Central = table.Column<string>(type: "SDO_GEOMETRY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Municipio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Zona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome_Zona = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Geometry = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cord_Central = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Municipio_Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Zona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Zona_TBL_Municipio_Municipio_Id",
                        column: x => x.Municipio_Id,
                        principalTable: "TBL_Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Distrito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Cd_Dist = table.Column<int>(type: "NUMBER(10)", maxLength: 7, nullable: false),
                    Nm_Dist = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Cord_Central = table.Column<string>(type: "SDO_GEOMETRY", nullable: false),
                    Zona_Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Distrito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Distrito_TBL_Zona_Zona_Id",
                        column: x => x.Zona_Id,
                        principalTable: "TBL_Zona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Previsao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Zona_Id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Temperatura_Max = table.Column<double>(type: "BINARY_DOUBLE", precision: 2, nullable: false),
                    Temperatura_Min = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Umidade_Max = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Umidade_Min = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Velocidade_Vento = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Volume_Precipitacao = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Tipo_Nuvem = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Pressao_Atm = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Previsao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Previsao_TBL_Zona_Zona_Id",
                        column: x => x.Zona_Id,
                        principalTable: "TBL_Zona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Previsao_Futura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Zona_Id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Data_Referencia = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Data_Futura = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Hora = table.Column<int>(type: "NUMBER(10)", maxLength: 2, nullable: false),
                    Temperatura_Max = table.Column<double>(type: "BINARY_DOUBLE", precision: 2, nullable: false),
                    Temperatura_Min = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Umidade_Max = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Umidade_Min = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Velocidade_Vento = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Volume_Precipitacao = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false),
                    Tipo_Nuvem = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Pressao_Atm = table.Column<double>(type: "BINARY_DOUBLE", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Previsao_Futura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Previsao_Futura_TBL_Zona_Zona_Id",
                        column: x => x.Zona_Id,
                        principalTable: "TBL_Zona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Distrito_Zona_Id",
                table: "TBL_Distrito",
                column: "Zona_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Previsao_Zona_Id",
                table: "TBL_Previsao",
                column: "Zona_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Previsao_Futura_Zona_Id",
                table: "TBL_Previsao_Futura",
                column: "Zona_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Zona_Municipio_Id",
                table: "TBL_Zona",
                column: "Municipio_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Distrito");

            migrationBuilder.DropTable(
                name: "TBL_Previsao");

            migrationBuilder.DropTable(
                name: "TBL_Previsao_Futura");

            migrationBuilder.DropTable(
                name: "TBL_Zona");

            migrationBuilder.DropTable(
                name: "TBL_Municipio");
        }
    }
}
