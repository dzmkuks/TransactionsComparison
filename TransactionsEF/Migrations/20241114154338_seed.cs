using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TransactionsEF.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Description", "ProcessedAt" },
                values: new object[,]
                {
                    { 1, 22.5f, " add videos", new DateTime(2009, 6, 1, 13, 45, 30, 0, DateTimeKind.Unspecified) },
                    { 2, 12.5f, " add video", new DateTime(2009, 6, 15, 13, 45, 11, 432, DateTimeKind.Unspecified).AddTicks(5111) },
                    { 3, 23.1f, " Add videos", new DateTime(2009, 7, 14, 13, 45, 21, 717, DateTimeKind.Unspecified).AddTicks(2232) },
                    { 4, 6.5f, " Add Videos", new DateTime(2009, 6, 11, 13, 40, 50, 657, DateTimeKind.Unspecified).AddTicks(3231) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
