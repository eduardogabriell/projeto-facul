using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_facul.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarEstabelecimentoContexto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contexto",
                table: "Gastos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estabelecimento",
                table: "Gastos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contexto",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "Estabelecimento",
                table: "Gastos");
        }
    }
}
