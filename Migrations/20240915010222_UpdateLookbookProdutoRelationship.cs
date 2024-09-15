using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lookbook_dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLookbookProdutoRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookbookProduto_Lookbooks_LookbooksId",
                table: "LookbookProduto");

            migrationBuilder.RenameColumn(
                name: "LookbooksId",
                table: "LookbookProduto",
                newName: "LookbookId");

            migrationBuilder.AddForeignKey(
                name: "FK_LookbookProduto_Lookbooks_LookbookId",
                table: "LookbookProduto",
                column: "LookbookId",
                principalTable: "Lookbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookbookProduto_Lookbooks_LookbookId",
                table: "LookbookProduto");

            migrationBuilder.RenameColumn(
                name: "LookbookId",
                table: "LookbookProduto",
                newName: "LookbooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_LookbookProduto_Lookbooks_LookbooksId",
                table: "LookbookProduto",
                column: "LookbooksId",
                principalTable: "Lookbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
