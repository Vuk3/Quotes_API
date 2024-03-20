using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes_API.Migrations
{
    public partial class TagsUpdate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Quotes_QuoteId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_QuoteId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "QuoteTag",
                columns: table => new
                {
                    QuotesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteTag", x => new { x.QuotesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_QuoteTag_Quotes_QuotesId",
                        column: x => x.QuotesId,
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuoteTag_TagsId",
                table: "QuoteTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuoteTag");

            migrationBuilder.AddColumn<int>(
                name: "QuoteId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_QuoteId",
                table: "Tags",
                column: "QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Quotes_QuoteId",
                table: "Tags",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id");
        }
    }
}
