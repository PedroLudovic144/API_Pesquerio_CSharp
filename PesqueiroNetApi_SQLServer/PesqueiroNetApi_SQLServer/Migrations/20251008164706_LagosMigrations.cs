using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PesqueiroNetApi.Migrations
{
    /// <inheritdoc />
    public partial class LagosMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EspeciesLagos_Lagos_LagoIdLago",
                table: "EspeciesLagos");

            migrationBuilder.DropTable(
                name: "PeixesClientes");

            migrationBuilder.DropTable(
                name: "PeixesCapturados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lagos",
                table: "Lagos");

            migrationBuilder.RenameTable(
                name: "Lagos",
                newName: "Lago");

            migrationBuilder.AddColumn<int>(
                name: "IdPesqueiro",
                table: "Lago",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PesqueiroIdPesqueiro",
                table: "Lago",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TamanhoLago",
                table: "Lago",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lago",
                table: "Lago",
                column: "IdLago");

            migrationBuilder.CreateTable(
                name: "Peixe",
                columns: table => new
                {
                    IdPeixe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Especie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PesoMedio = table.Column<double>(type: "float", nullable: true),
                    IdLago = table.Column<int>(type: "int", nullable: false),
                    LagoIdLago = table.Column<int>(type: "int", nullable: true),
                    EspecieIdEspecie = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peixe", x => x.IdPeixe);
                    table.ForeignKey(
                        name: "FK_Peixe_Especies_EspecieIdEspecie",
                        column: x => x.EspecieIdEspecie,
                        principalTable: "Especies",
                        principalColumn: "IdEspecie");
                    table.ForeignKey(
                        name: "FK_Peixe_Lago_LagoIdLago",
                        column: x => x.LagoIdLago,
                        principalTable: "Lago",
                        principalColumn: "IdLago");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lago_PesqueiroIdPesqueiro",
                table: "Lago",
                column: "PesqueiroIdPesqueiro");

            migrationBuilder.CreateIndex(
                name: "IX_Peixe_EspecieIdEspecie",
                table: "Peixe",
                column: "EspecieIdEspecie");

            migrationBuilder.CreateIndex(
                name: "IX_Peixe_LagoIdLago",
                table: "Peixe",
                column: "LagoIdLago");

            migrationBuilder.AddForeignKey(
                name: "FK_EspeciesLagos_Lago_LagoIdLago",
                table: "EspeciesLagos",
                column: "LagoIdLago",
                principalTable: "Lago",
                principalColumn: "IdLago");

            migrationBuilder.AddForeignKey(
                name: "FK_Lago_Pesqueiros_PesqueiroIdPesqueiro",
                table: "Lago",
                column: "PesqueiroIdPesqueiro",
                principalTable: "Pesqueiros",
                principalColumn: "IdPesqueiro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EspeciesLagos_Lago_LagoIdLago",
                table: "EspeciesLagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Lago_Pesqueiros_PesqueiroIdPesqueiro",
                table: "Lago");

            migrationBuilder.DropTable(
                name: "Peixe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lago",
                table: "Lago");

            migrationBuilder.DropIndex(
                name: "IX_Lago_PesqueiroIdPesqueiro",
                table: "Lago");

            migrationBuilder.DropColumn(
                name: "IdPesqueiro",
                table: "Lago");

            migrationBuilder.DropColumn(
                name: "PesqueiroIdPesqueiro",
                table: "Lago");

            migrationBuilder.DropColumn(
                name: "TamanhoLago",
                table: "Lago");

            migrationBuilder.RenameTable(
                name: "Lago",
                newName: "Lagos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lagos",
                table: "Lagos",
                column: "IdLago");

            migrationBuilder.CreateTable(
                name: "PeixesCapturados",
                columns: table => new
                {
                    IdPeixeCapturado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspecieIdEspecie = table.Column<int>(type: "int", nullable: true),
                    DataPeixeCapturado = table.Column<DateOnly>(type: "date", nullable: false),
                    IdEspecie = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeixesCapturados", x => x.IdPeixeCapturado);
                    table.ForeignKey(
                        name: "FK_PeixesCapturados_Especies_EspecieIdEspecie",
                        column: x => x.EspecieIdEspecie,
                        principalTable: "Especies",
                        principalColumn: "IdEspecie");
                });

            migrationBuilder.CreateTable(
                name: "PeixesClientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPeixeCapturado = table.Column<int>(type: "int", nullable: false),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: true),
                    PeixeCapturadoIdPeixeCapturado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeixesClientes", x => new { x.IdCliente, x.IdPeixeCapturado });
                    table.ForeignKey(
                        name: "FK_PeixesClientes_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_PeixesClientes_PeixesCapturados_PeixeCapturadoIdPeixeCapturado",
                        column: x => x.PeixeCapturadoIdPeixeCapturado,
                        principalTable: "PeixesCapturados",
                        principalColumn: "IdPeixeCapturado");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeixesCapturados_EspecieIdEspecie",
                table: "PeixesCapturados",
                column: "EspecieIdEspecie");

            migrationBuilder.CreateIndex(
                name: "IX_PeixesClientes_ClienteIdCliente",
                table: "PeixesClientes",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_PeixesClientes_PeixeCapturadoIdPeixeCapturado",
                table: "PeixesClientes",
                column: "PeixeCapturadoIdPeixeCapturado");

            migrationBuilder.AddForeignKey(
                name: "FK_EspeciesLagos_Lagos_LagoIdLago",
                table: "EspeciesLagos",
                column: "LagoIdLago",
                principalTable: "Lagos",
                principalColumn: "IdLago");
        }
    }
}
