using Microsoft.EntityFrameworkCore.Migrations;

namespace coinStack.Server.Migrations
{
    public partial class PortfolioCoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchlistCoins");

            migrationBuilder.DropTable(
                name: "UserWatchlists");

            migrationBuilder.CreateTable(
                name: "PortfolioCoins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserPortfolioId = table.Column<int>(type: "int", nullable: false),
                    CoinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCoins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioCoins_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioCoins_UserPortfolios_UserPortfolioId",
                        column: x => x.UserPortfolioId,
                        principalTable: "UserPortfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCoins_CoinId",
                table: "PortfolioCoins",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCoins_UserPortfolioId",
                table: "PortfolioCoins",
                column: "UserPortfolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioCoins");

            migrationBuilder.CreateTable(
                name: "UserWatchlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentlySelected = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWatchlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchlistCoins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoinId = table.Column<int>(type: "int", nullable: false),
                    UserWatchlistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchlistCoins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchlistCoins_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchlistCoins_UserWatchlists_UserWatchlistId",
                        column: x => x.UserWatchlistId,
                        principalTable: "UserWatchlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchlists_UserId",
                table: "UserWatchlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistCoins_CoinId",
                table: "WatchlistCoins",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistCoins_UserWatchlistId",
                table: "WatchlistCoins",
                column: "UserWatchlistId");
        }
    }
}
