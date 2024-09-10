using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lookbook_dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lookbooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookbooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CategoriaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Categoria = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookbookProduto",
                columns: table => new
                {
                    LookbooksId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProdutosId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookbookProduto", x => new { x.LookbooksId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_LookbookProduto_Lookbooks_LookbooksId",
                        column: x => x.LookbooksId,
                        principalTable: "Lookbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LookbookProduto_Produtos_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LookbookProduto_ProdutosId",
                table: "LookbookProduto",
                column: "ProdutosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LookbookProduto");

            migrationBuilder.DropTable(
                name: "Lookbooks");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
