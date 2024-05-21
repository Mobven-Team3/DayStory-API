using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DayStoryDB");

            migrationBuilder.CreateTable(
                name: "ArtStyle",
                schema: "DayStoryDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtStyle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "DayStoryDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePicturePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Gender = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "User"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaySummary",
                schema: "DayStoryDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "FORMAT(GETDATE(), 'dd-MM-yyyy')"),
                    ImagePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ArtStyleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaySummary_ArtStyle_ArtStyleId",
                        column: x => x.ArtStyleId,
                        principalSchema: "DayStoryDB",
                        principalTable: "ArtStyle",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DaySummary_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "DayStoryDB",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "DayStoryDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "FORMAT(GETDATE(), 'dd-MM-yyyy')"),
                    Time = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DaySummaryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_DaySummary_DaySummaryId",
                        column: x => x.DaySummaryId,
                        principalSchema: "DayStoryDB",
                        principalTable: "DaySummary",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Event_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "DayStoryDB",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaySummary_ArtStyleId",
                schema: "DayStoryDB",
                table: "DaySummary",
                column: "ArtStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_DaySummary_UserId",
                schema: "DayStoryDB",
                table: "DaySummary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_DaySummaryId",
                schema: "DayStoryDB",
                table: "Event",
                column: "DaySummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserId",
                schema: "DayStoryDB",
                table: "Event",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "DayStoryDB",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                schema: "DayStoryDB",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event",
                schema: "DayStoryDB");

            migrationBuilder.DropTable(
                name: "DaySummary",
                schema: "DayStoryDB");

            migrationBuilder.DropTable(
                name: "ArtStyle",
                schema: "DayStoryDB");

            migrationBuilder.DropTable(
                name: "User",
                schema: "DayStoryDB");
        }
    }
}
