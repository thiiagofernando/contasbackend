using Microsoft.EntityFrameworkCore.Migrations;

namespace CadastroConta.Data.Migrations
{
    public partial class PagamentoRealizado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PagamentoRealizado",
                table: "Conta",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagamentoRealizado",
                table: "Conta");
        }
    }
}
