using Microsoft.EntityFrameworkCore.Migrations;

namespace coinStack.Server.Migrations
{
    public partial class Coins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioCoins_Coin_Coinid",
                table: "PortfolioCoins");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Coin_Coinid",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coin",
                table: "Coin");

            migrationBuilder.RenameTable(
                name: "Coin",
                newName: "Coins");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coins",
                table: "Coins",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioCoins_Coins_Coinid",
                table: "PortfolioCoins",
                column: "Coinid",
                principalTable: "Coins",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Coins_Coinid",
                table: "Transactions",
                column: "Coinid",
                principalTable: "Coins",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioCoins_Coins_Coinid",
                table: "PortfolioCoins");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Coins_Coinid",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coins",
                table: "Coins");

            migrationBuilder.RenameTable(
                name: "Coins",
                newName: "Coin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coin",
                table: "Coin",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioCoins_Coin_Coinid",
                table: "PortfolioCoins",
                column: "Coinid",
                principalTable: "Coin",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Coin_Coinid",
                table: "Transactions",
                column: "Coinid",
                principalTable: "Coin",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
