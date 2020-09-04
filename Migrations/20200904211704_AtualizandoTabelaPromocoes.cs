using Microsoft.EntityFrameworkCore.Migrations;

namespace projetoMarket.Migrations
{
    public partial class AtualizandoTabelaPromocoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Promocoes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promocoes_ProdutoId",
                table: "Promocoes",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promocoes_Produtos_ProdutoId",
                table: "Promocoes",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promocoes_Produtos_ProdutoId",
                table: "Promocoes");

            migrationBuilder.DropIndex(
                name: "IX_Promocoes_ProdutoId",
                table: "Promocoes");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Promocoes");
        }
    }
}
