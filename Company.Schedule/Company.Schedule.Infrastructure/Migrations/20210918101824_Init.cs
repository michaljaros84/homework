using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Schedule.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Small = table.Column<bool>(type: "bit", nullable: false),
                    Medium = table.Column<bool>(type: "bit", nullable: false),
                    Large = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    MarketId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketDays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketId = table.Column<long>(type: "bigint", nullable: false),
                    DaysOffset = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketDays_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledDates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledDates_Companies_CompanyEntityId",
                        column: x => x.CompanyEntityId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Markets",
                columns: new[] { "Id", "Large", "Medium", "Name", "Small" },
                values: new object[,]
                {
                    { 1L, true, true, "Denmark", true },
                    { 2L, true, true, "Norway", true },
                    { 3L, false, true, "Sweden", true },
                    { 4L, true, false, "Finland", false }
                });

            migrationBuilder.InsertData(
                table: "MarketDays",
                columns: new[] { "Id", "DaysOffset", "MarketId" },
                values: new object[,]
                {
                    { 1L, 1, 1L },
                    { 16L, 10, 4L },
                    { 15L, 5, 4L },
                    { 14L, 1, 4L },
                    { 13L, 28, 3L },
                    { 12L, 14, 3L },
                    { 11L, 7, 3L },
                    { 10L, 1, 3L },
                    { 9L, 20, 2L },
                    { 8L, 10, 2L },
                    { 7L, 5, 2L },
                    { 6L, 1, 2L },
                    { 5L, 20, 1L },
                    { 4L, 15, 1L },
                    { 3L, 10, 1L },
                    { 2L, 5, 1L },
                    { 17L, 15, 4L },
                    { 18L, 20, 4L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_MarketId",
                table: "Companies",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketDays_MarketId",
                table: "MarketDays",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDates_CompanyEntityId",
                table: "ScheduledDates",
                column: "CompanyEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketDays");

            migrationBuilder.DropTable(
                name: "ScheduledDates");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Markets");
        }
    }
}
