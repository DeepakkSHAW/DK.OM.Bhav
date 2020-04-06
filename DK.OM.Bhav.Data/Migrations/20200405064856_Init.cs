using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DK.OM.Bhav.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BhavStockId = table.Column<int>(nullable: false),
                    Open = table.Column<double>(nullable: false),
                    High = table.Column<double>(nullable: false),
                    Low = table.Column<double>(nullable: false),
                    Close = table.Column<double>(nullable: false),
                    Last = table.Column<double>(nullable: false),
                    PreClose = table.Column<double>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StockName = table.Column<string>(nullable: true),
                    StockFullName = table.Column<string>(nullable: true),
                    BSECode = table.Column<string>(nullable: true),
                    NSECode = table.Column<string>(nullable: true),
                    StockGroup = table.Column<string>(nullable: true),
                    StockType = table.Column<string>(nullable: true),
                    inDate = table.Column<DateTime>(nullable: false),
                    updateDate = table.Column<DateTime>(nullable: false),
                    BhavStockPricesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Prices_BhavStockPricesId",
                        column: x => x.BhavStockPricesId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BhavStockPricesId",
                table: "Stocks",
                column: "BhavStockPricesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
