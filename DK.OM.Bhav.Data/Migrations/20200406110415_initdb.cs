using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DK.OM.Bhav.Data.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BSEStockPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BhavBSEStockId = table.Column<int>(nullable: false),
                    Open = table.Column<double>(nullable: false),
                    High = table.Column<double>(nullable: false),
                    Low = table.Column<double>(nullable: false),
                    Close = table.Column<double>(nullable: false),
                    Last = table.Column<double>(nullable: false),
                    PreClose = table.Column<double>(nullable: false),
                    Turnover = table.Column<long>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BSEStockPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BSEStocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BSECode = table.Column<string>(nullable: true),
                    StockName = table.Column<string>(nullable: true),
                    StockGroup = table.Column<string>(nullable: true),
                    StockType = table.Column<string>(nullable: true),
                    inDate = table.Column<DateTime>(nullable: false),
                    updateDate = table.Column<DateTime>(nullable: false),
                    BhavBSEStockPricesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BSEStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BSEStocks_BSEStockPrices_BhavBSEStockPricesId",
                        column: x => x.BhavBSEStockPricesId,
                        principalTable: "BSEStockPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BSEStocks_BhavBSEStockPricesId",
                table: "BSEStocks",
                column: "BhavBSEStockPricesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BSEStocks");

            migrationBuilder.DropTable(
                name: "BSEStockPrices");
        }
    }
}
