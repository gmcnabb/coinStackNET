using Microsoft.EntityFrameworkCore.Migrations;

namespace coinStack.Server.Migrations
{
    public partial class CoinsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coin_Watchlists_WatchlistId",
                table: "Coin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coin",
                table: "Coin");

            migrationBuilder.RenameTable(
                name: "Coin",
                newName: "Coins");

            migrationBuilder.RenameIndex(
                name: "IX_Coin_WatchlistId",
                table: "Coins",
                newName: "IX_Coins_WatchlistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coins",
                table: "Coins",
                column: "CoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coins_Watchlists_WatchlistId",
                table: "Coins",
                column: "WatchlistId",
                principalTable: "Watchlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coins_Watchlists_WatchlistId",
                table: "Coins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coins",
                table: "Coins");

            migrationBuilder.RenameTable(
                name: "Coins",
                newName: "Coin");

            migrationBuilder.RenameIndex(
                name: "IX_Coins_WatchlistId",
                table: "Coin",
                newName: "IX_Coin_WatchlistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coin",
                table: "Coin",
                column: "CoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coin_Watchlists_WatchlistId",
                table: "Coin",
                column: "WatchlistId",
                principalTable: "Watchlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
