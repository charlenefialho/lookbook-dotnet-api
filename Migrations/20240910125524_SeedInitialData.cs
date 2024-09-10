using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace lookbook_dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookbooks",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Look de Verão" },
                    { 2, "Look de Inverno" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Nome", "Preco", "Tags" },
                values: new object[,]
                {
                    { 1, "Camiseta Branca", 29.989999999999998, "[\"Casual\",\"Ver\\u00E3o\"]" },
                    { 2, "Calça Jeans", 79.989999999999995, "[\"Outono\",\"Dur\\u00E1vel\"]" },
                    { 3, "Jaqueta de Couro", 199.99000000000001, "[\"Inverno\",\"Estiloso\"]" }
                });

            migrationBuilder.InsertData(
                table: "LookbookProduto",
                columns: new[] { "LookbookId", "ProdutoId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LookbookProduto",
                keyColumns: new[] { "LookbookId", "ProdutoId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "LookbookProduto",
                keyColumns: new[] { "LookbookId", "ProdutoId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "LookbookProduto",
                keyColumns: new[] { "LookbookId", "ProdutoId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Lookbooks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lookbooks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
