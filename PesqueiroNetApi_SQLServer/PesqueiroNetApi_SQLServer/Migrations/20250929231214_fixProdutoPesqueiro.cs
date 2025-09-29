using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PesqueiroNetApi.Migrations
{
    /// <inheritdoc />
    public partial class fixProdutoPesqueiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Pesqueiros_PesqueiroIdPesqueiro",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_PesqueiroIdPesqueiro",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PesqueiroIdPesqueiro",
                table: "Produtos");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IdPesqueiro",
                table: "Produtos",
                column: "IdPesqueiro");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Pesqueiros_IdPesqueiro",
                table: "Produtos",
                column: "IdPesqueiro",
                principalTable: "Pesqueiros",
                principalColumn: "IdPesqueiro",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Pesqueiros_IdPesqueiro",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_IdPesqueiro",
                table: "Produtos");

            migrationBuilder.AddColumn<int>(
                name: "PesqueiroIdPesqueiro",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_PesqueiroIdPesqueiro",
                table: "Produtos",
                column: "PesqueiroIdPesqueiro");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Pesqueiros_PesqueiroIdPesqueiro",
                table: "Produtos",
                column: "PesqueiroIdPesqueiro",
                principalTable: "Pesqueiros",
                principalColumn: "IdPesqueiro",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
