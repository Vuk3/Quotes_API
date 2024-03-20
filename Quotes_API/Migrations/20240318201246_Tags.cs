using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes_API.Migrations
{
    public partial class Tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuoteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Value);
                    table.ForeignKey(
                        name: "FK_Tag_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_QuoteId",
                table: "Tag",
                column: "QuoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
