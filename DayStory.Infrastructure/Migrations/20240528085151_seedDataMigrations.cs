using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DayStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedDataMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                columns: new[] { "Id", "DeletedOn", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, "Realist", null },
                    { 2, null, "Surrealist", null },
                    { 3, null, "Minimalist", null },
                    { 4, null, "Retro/Vintage", null },
                    { 5, null, "Steampunk", null },
                    { 6, null, "Cyberpunk", null },
                    { 7, null, "Futuristic", null },
                    { 8, null, "Baroque", null },
                    { 9, null, "Gothic", null },
                    { 10, null, "Pop Art", null },
                    { 11, null, "Abstract", null },
                    { 12, null, "Art Deco", null },
                    { 13, null, "Fantasy", null },
                    { 14, null, "Digital Art", null },
                    { 15, null, "Cinematic", null },
                    { 16, null, "Anime", null },
                    { 17, null, "Cartoonist", null },
                    { 18, null, "Comic", null },
                    { 19, null, "Retro Pop", null },
                    { 20, null, "Logo", null },
                    { 21, null, "Whimsical", null },
                    { 22, null, "Horror", null },
                    { 23, null, "Monster", null },
                    { 24, null, "Figure", null },
                    { 25, null, "Retro Sci-Fi", null },
                    { 26, null, "Dark Fantasy", null },
                    { 27, null, "Dreamwave", null },
                    { 28, null, "Mystical", null },
                    { 29, null, "Expressionism", null },
                    { 30, null, "Flora", null },
                    { 31, null, "Daydream", null },
                    { 32, null, "Radioactive", null },
                    { 33, null, "Starry", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "DayStoryDB",
                table: "ArtStyle",
                keyColumn: "Id",
                keyValue: 33);
        }
    }
}
