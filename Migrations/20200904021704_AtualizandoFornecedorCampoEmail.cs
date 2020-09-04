using Microsoft.EntityFrameworkCore.Migrations;

namespace projetoMarket.Migrations
{
    public partial class AtualizandoFornecedorCampoEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Fornecedores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Fornecedores");
        }
    }
}
