using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdessoRideShare.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCityGridCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 2, 8 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 10, 5 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 6, 2 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 3, 7 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 5, 6 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 8, 6 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 9, 4 });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "GridX", "GridY", "Name" },
                values: new object[,]
                {
                    { 8, 5, 3, "Bursa" },
                    { 9, 6, 5, "Kütahya" },
                    { 10, 4, 4, "Balıkesir" },
                    { 11, 7, 1, "Kocaeli" },
                    { 12, 8, 2, "Sakarya" },
                    { 13, 4, 8, "Denizli" },
                    { 14, 12, 7, "Konya" },
                    { 15, 8, 9, "Antalya" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 5, 2 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 9, 3 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 3, 2 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "GridX", "GridY" },
                values: new object[] { 4, 2 });
        }
    }
}
