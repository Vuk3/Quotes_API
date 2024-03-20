using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes_API.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginUserQuote");

            migrationBuilder.AddColumn<string>(
                name: "LoginUserUserName",
                table: "UsersAndQuotes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersAndQuotes_LoginUserUserName",
                table: "UsersAndQuotes",
                column: "LoginUserUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAndQuotes_Quotes_QuoteId",
                table: "UsersAndQuotes",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAndQuotes_Users_LoginUserUserName",
                table: "UsersAndQuotes",
                column: "LoginUserUserName",
                principalTable: "Users",
                principalColumn: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersAndQuotes_Quotes_QuoteId",
                table: "UsersAndQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersAndQuotes_Users_LoginUserUserName",
                table: "UsersAndQuotes");

            migrationBuilder.DropIndex(
                name: "IX_UsersAndQuotes_LoginUserUserName",
                table: "UsersAndQuotes");

            migrationBuilder.DropColumn(
                name: "LoginUserUserName",
                table: "UsersAndQuotes");

            migrationBuilder.CreateTable(
                name: "LoginUserQuote",
                columns: table => new
                {
                    ReactedQuotesId = table.Column<int>(type: "int", nullable: false),
                    ReactionsByUsersUserName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginUserQuote", x => new { x.ReactedQuotesId, x.ReactionsByUsersUserName });
                    table.ForeignKey(
                        name: "FK_LoginUserQuote_Quotes_ReactedQuotesId",
                        column: x => x.ReactedQuotesId,
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoginUserQuote_Users_ReactionsByUsersUserName",
                        column: x => x.ReactionsByUsersUserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginUserQuote_ReactionsByUsersUserName",
                table: "LoginUserQuote",
                column: "ReactionsByUsersUserName");
        }
    }
}
