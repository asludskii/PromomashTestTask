using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromomashTestTask.Data.Migrations.SQLiteMigrations
{
    public partial class SeedDataPlaceholderPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PolicyVersions",
                columns: new[] { "Id", "CreatedDateTimeUtc", "PolicyText" },
                values: new object[] { new Guid("3ba5242a-a483-571b-bdfe-084e46348c73"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Putting pineapple on pizza disqualifies it as food." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PolicyVersions",
                keyColumn: "Id",
                keyValue: new Guid("3ba5242a-a483-571b-bdfe-084e46348c73"));
        }
    }
}
