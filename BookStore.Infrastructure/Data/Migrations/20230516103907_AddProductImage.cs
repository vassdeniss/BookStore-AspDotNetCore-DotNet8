using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1aede173-5896-43c8-a89f-b056403093bb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1fe99f82-bc3b-496a-b329-05808869df3f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("711dcbd5-f1ba-4453-89ac-626918452944"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("72557c0a-b755-4c2c-94b9-1afdfe7cef7f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc0e866b-69f1-459c-94f8-dba3d344e391"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ef3d6021-4408-4bd9-a297-d4aa2918fa30"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1e1dcf31-80b2-4346-812f-f9c6a2faca2b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("76667001-35f7-49f5-ac7e-19337a4c43d5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d4d6b6b0-9427-41cd-ae03-d8c278e943bf"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { new Guid("7e9203c2-de44-4ea1-b3cc-3dc4680fd1f8"), 1, "Fantasy" },
                    { new Guid("ab42220e-0698-480e-bb9f-592b352ab7fa"), 3, "Action" },
                    { new Guid("fe6100e6-3f3f-434d-92b7-eb163b733a04"), 2, "Sci-fi" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { new Guid("09e809f8-d65f-40df-ba20-6530497d67d3"), "Julian Button", new Guid("fe6100e6-3f3f-434d-92b7-eb163b733a04"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "RITO5555501", "", 55.0, 50.0, 35.0, 40.0, "Vanish in the Sunset" },
                    { new Guid("23752b87-c7b3-4778-b41c-25d87b4c2914"), "Billy Spark", new Guid("7e9203c2-de44-4ea1-b3cc-3dc4680fd1f8"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "SWD9999001", "", 99.0, 90.0, 80.0, 85.0, "Fortune of Time" },
                    { new Guid("45e61c94-26f3-45ec-b4d6-41f2a3c12b68"), "Nancy Hoover", new Guid("7e9203c2-de44-4ea1-b3cc-3dc4680fd1f8"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "CAW777777701", "", 40.0, 30.0, 20.0, 25.0, "Dark Skies" },
                    { new Guid("c2ea6208-fd54-414b-af9d-46441d599648"), "Abby Muscles", new Guid("fe6100e6-3f3f-434d-92b7-eb163b733a04"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "WS3333333301", "", 70.0, 65.0, 55.0, 60.0, "Cotton Candy" },
                    { new Guid("ec71d421-222a-40a5-a7f3-a57dcbf8a629"), "Laura Phantom", new Guid("ab42220e-0698-480e-bb9f-592b352ab7fa"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "FOT000000001", "", 25.0, 23.0, 20.0, 22.0, "Leaves and Wonders" },
                    { new Guid("edcaf335-73a6-4231-84d4-0f5661a04a80"), "Ron Parker", new Guid("ab42220e-0698-480e-bb9f-592b352ab7fa"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "SOTJ1111111101", "", 30.0, 27.0, 20.0, 25.0, "Rock in the Ocean" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("09e809f8-d65f-40df-ba20-6530497d67d3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("23752b87-c7b3-4778-b41c-25d87b4c2914"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("45e61c94-26f3-45ec-b4d6-41f2a3c12b68"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c2ea6208-fd54-414b-af9d-46441d599648"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ec71d421-222a-40a5-a7f3-a57dcbf8a629"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("edcaf335-73a6-4231-84d4-0f5661a04a80"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7e9203c2-de44-4ea1-b3cc-3dc4680fd1f8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ab42220e-0698-480e-bb9f-592b352ab7fa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fe6100e6-3f3f-434d-92b7-eb163b733a04"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { new Guid("1e1dcf31-80b2-4346-812f-f9c6a2faca2b"), 1, "Fantasy" },
                    { new Guid("76667001-35f7-49f5-ac7e-19337a4c43d5"), 3, "Action" },
                    { new Guid("d4d6b6b0-9427-41cd-ae03-d8c278e943bf"), 2, "Sci-fi" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { new Guid("1aede173-5896-43c8-a89f-b056403093bb"), "Billy Spark", new Guid("1e1dcf31-80b2-4346-812f-f9c6a2faca2b"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "SWD9999001", 99.0, 90.0, 80.0, 85.0, "Fortune of Time" },
                    { new Guid("1fe99f82-bc3b-496a-b329-05808869df3f"), "Abby Muscles", new Guid("d4d6b6b0-9427-41cd-ae03-d8c278e943bf"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "WS3333333301", 70.0, 65.0, 55.0, 60.0, "Cotton Candy" },
                    { new Guid("711dcbd5-f1ba-4453-89ac-626918452944"), "Julian Button", new Guid("d4d6b6b0-9427-41cd-ae03-d8c278e943bf"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "RITO5555501", 55.0, 50.0, 35.0, 40.0, "Vanish in the Sunset" },
                    { new Guid("72557c0a-b755-4c2c-94b9-1afdfe7cef7f"), "Ron Parker", new Guid("76667001-35f7-49f5-ac7e-19337a4c43d5"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "SOTJ1111111101", 30.0, 27.0, 20.0, 25.0, "Rock in the Ocean" },
                    { new Guid("dc0e866b-69f1-459c-94f8-dba3d344e391"), "Laura Phantom", new Guid("76667001-35f7-49f5-ac7e-19337a4c43d5"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "FOT000000001", 25.0, 23.0, 20.0, 22.0, "Leaves and Wonders" },
                    { new Guid("ef3d6021-4408-4bd9-a297-d4aa2918fa30"), "Nancy Hoover", new Guid("1e1dcf31-80b2-4346-812f-f9c6a2faca2b"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "CAW777777701", 40.0, 30.0, 20.0, 25.0, "Dark Skies" }
                });
        }
    }
}
