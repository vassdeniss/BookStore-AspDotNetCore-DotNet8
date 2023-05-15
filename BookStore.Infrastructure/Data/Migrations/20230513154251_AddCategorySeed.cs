using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategorySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { new Guid("75a25bb8-a13a-488a-956c-19d3a8f5f238"), 1, "Fantasy" },
                    { new Guid("a142ae3a-3f8c-444f-a7ea-ca6b49fdea90"), 3, "Action" },
                    { new Guid("c15d7d6c-5314-4da4-b8d7-d20345433e58"), 2, "Sci-fi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("75a25bb8-a13a-488a-956c-19d3a8f5f238"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a142ae3a-3f8c-444f-a7ea-ca6b49fdea90"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c15d7d6c-5314-4da4-b8d7-d20345433e58"));
        }
    }
}
