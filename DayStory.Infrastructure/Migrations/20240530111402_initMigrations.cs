using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DayStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DayStoryDb");

            migrationBuilder.CreateTable(
                name: "ArtStyle",
                schema: "DayStoryDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtStyle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "DayStoryDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HashedPassword = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    BirthDate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProfilePicturePath = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Gender = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "User"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaySummary",
                schema: "DayStoryDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "TO_CHAR(NOW(), 'DD-MM-YYYY')"),
                    ImagePath = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Summary = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ArtStyleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaySummary_ArtStyle_ArtStyleId",
                        column: x => x.ArtStyleId,
                        principalSchema: "DayStoryDb",
                        principalTable: "ArtStyle",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DaySummary_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "DayStoryDb",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "DayStoryDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true),
                    Date = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "TO_CHAR(NOW(), 'DD-MM-YYYY')"),
                    Time = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DaySummaryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_DaySummary_DaySummaryId",
                        column: x => x.DaySummaryId,
                        principalSchema: "DayStoryDb",
                        principalTable: "DaySummary",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Event_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "DayStoryDb",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "DayStoryDb",
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

            migrationBuilder.InsertData(
                schema: "DayStoryDb",
                table: "User",
                columns: new[] { "Id", "BirthDate", "CreatedOn", "DeletedOn", "Email", "FirstName", "Gender", "HashedPassword", "LastLogin", "LastName", "ProfilePicturePath", "Role", "UpdatedOn", "Username" },
                values: new object[,]
                {
                    { 1, "00-00-0000", new DateTime(2024, 5, 30, 11, 14, 2, 400, DateTimeKind.Utc).AddTicks(8750), null, "admin", "admin", 0, "AQAAAAIAAYagAAAAEMAjbf9FW8x1DZZ+AyQXeu64godJj2gAwx6EPpTyTL1El6DlT1fXmkdw/+a81zac3g==", null, "admin", null, "Admin", null, "admin" },
                    { 2, "24-03-1999", new DateTime(2024, 5, 30, 11, 14, 2, 400, DateTimeKind.Utc).AddTicks(8759), null, "nisa@gmail.com", "Nisa", 2, "AQAAAAIAAYagAAAAEEbO/nyiGYmwOTIe8lSZOx5V0fA3uJCJoajax2zFl9ZSSMbM10M27yrBbwzUPCc6gg==", null, "Turhan", null, "User", null, "nisatur" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaySummary_ArtStyleId",
                schema: "DayStoryDb",
                table: "DaySummary",
                column: "ArtStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_DaySummary_UserId",
                schema: "DayStoryDb",
                table: "DaySummary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_DaySummaryId",
                schema: "DayStoryDb",
                table: "Event",
                column: "DaySummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserId",
                schema: "DayStoryDb",
                table: "Event",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "DayStoryDb",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                schema: "DayStoryDb",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event",
                schema: "DayStoryDb");

            migrationBuilder.DropTable(
                name: "DaySummary",
                schema: "DayStoryDb");

            migrationBuilder.DropTable(
                name: "ArtStyle",
                schema: "DayStoryDb");

            migrationBuilder.DropTable(
                name: "User",
                schema: "DayStoryDb");
        }
    }
}
