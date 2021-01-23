using Microsoft.EntityFrameworkCore.Migrations;

namespace coinStack.Server.Migrations
{
    public partial class dropCoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioCoins_Coins_CoinId",
                table: "PortfolioCoins");

            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioCoins_CoinId",
                table: "PortfolioCoins");

            migrationBuilder.DropColumn(
                name: "CoinId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CoinId",
                table: "PortfolioCoins");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoinId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoinId",
                table: "PortfolioCoins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeckoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeckoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeckoSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCoins_CoinId",
                table: "PortfolioCoins",
                column: "CoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioCoins_Coins_CoinId",
                table: "PortfolioCoins",
                column: "CoinId",
                principalTable: "Coins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
