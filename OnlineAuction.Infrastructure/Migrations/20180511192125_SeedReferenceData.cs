using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnlineAuction.Infrastructure.Migrations
{
    public partial class SeedReferenceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories (CategoryName) VALUES ('Clothes')");
            migrationBuilder.Sql("INSERT INTO Categories (CategoryName) VALUES ('Books')");
            migrationBuilder.Sql("INSERT INTO Categories (CategoryName) VALUES ('Antiques')");
            migrationBuilder.Sql("INSERT INTO Categories (CategoryName) VALUES ('Jewelry')");

            migrationBuilder.Sql("INSERT INTO LotStates (LotStateName) VALUES ('Active')");
            migrationBuilder.Sql("INSERT INTO LotStates (LotStateName) VALUES ('Sold')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories WHERE CategoryName IN ('Clothes', 'Books', 'Antiques', 'Jewelry'");
            migrationBuilder.Sql("DELETE FROM LotStates WHERE LotStateName IN ('Active', 'Sold')");
        }
    }
}
