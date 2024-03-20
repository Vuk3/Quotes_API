using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes_API.Migrations
{
    public partial class TagsUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Quotes_QuoteId",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_QuoteId",
                table: "Tags",
                newName: "IX_Tags_QuoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Quotes_QuoteId",
                table: "Tags",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Quotes_QuoteId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_QuoteId",
                table: "Tag",
                newName: "IX_Tag_QuoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Quotes_QuoteId",
                table: "Tag",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id");
        }
    }
}
