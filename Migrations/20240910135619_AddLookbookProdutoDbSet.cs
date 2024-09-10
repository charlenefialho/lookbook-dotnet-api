using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lookbook_dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class AddLookbookProdutoDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookbookProduto_Lookbooks_LookbookId",
                table: "LookbookProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_LookbookProduto_Produtos_ProdutoId",
                table: "LookbookProduto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookbookProduto",
                table: "LookbookProduto");

            migrationBuilder.RenameTable(
                name: "LookbookProduto",
                newName: "LookbookProdutos");

            migrationBuilder.RenameIndex(
                name: "IX_LookbookProduto_ProdutoId",
                table: "LookbookProdutos",
                newName: "IX_LookbookProdutos_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookbookProdutos",
                table: "LookbookProdutos",
                columns: new[] { "LookbookId", "ProdutoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LookbookProdutos_Lookbooks_LookbookId",
                table: "LookbookProdutos",
                column: "LookbookId",
                principalTable: "Lookbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LookbookProdutos_Produtos_ProdutoId",
                table: "LookbookProdutos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookbookProdutos_Lookbooks_LookbookId",
                table: "LookbookProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_LookbookProdutos_Produtos_ProdutoId",
                table: "LookbookProdutos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookbookProdutos",
                table: "LookbookProdutos");

            migrationBuilder.RenameTable(
                name: "LookbookProdutos",
                newName: "LookbookProduto");

            migrationBuilder.RenameIndex(
                name: "IX_LookbookProdutos_ProdutoId",
                table: "LookbookProduto",
                newName: "IX_LookbookProduto_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookbookProduto",
                table: "LookbookProduto",
                columns: new[] { "LookbookId", "ProdutoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LookbookProduto_Lookbooks_LookbookId",
                table: "LookbookProduto",
                column: "LookbookId",
                principalTable: "Lookbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LookbookProduto_Produtos_ProdutoId",
                table: "LookbookProduto",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
