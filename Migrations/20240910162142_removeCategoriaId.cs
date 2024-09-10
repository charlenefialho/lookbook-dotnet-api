using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lookbook_dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class removeCategoriaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Produtos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Produtos",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);
        }
    }
}
