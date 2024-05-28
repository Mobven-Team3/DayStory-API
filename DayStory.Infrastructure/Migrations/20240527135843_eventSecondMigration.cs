using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eventSecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "DayStoryDB",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "DayStoryDB",
                table: "Event");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "DayStoryDB",
                table: "Event",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "DayStoryDB",
                table: "Event",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
