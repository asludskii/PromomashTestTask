using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromomashTestTask.Data.Migrations.SQLiteMigrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("0cabb6d7-2c88-50b5-8108-2d03ff2b15da"), "Outland" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("4eab05b5-c6fa-5f31-9ae0-d9901ebdc673"), "Azeroth" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("53e050b1-cd32-5510-a813-dc3acf0e7e4c"), new Guid("0cabb6d7-2c88-50b5-8108-2d03ff2b15da"), "Hellfire Peninsula" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("643b0680-fe42-581a-9b29-bfac979992a7"), new Guid("4eab05b5-c6fa-5f31-9ae0-d9901ebdc673"), "Pandaria" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("6ac4b2fa-effc-5535-9e70-2cad13fadf1c"), new Guid("0cabb6d7-2c88-50b5-8108-2d03ff2b15da"), "Netherstorm" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("9463c17b-e669-5941-8d71-0d5e959b6d13"), new Guid("0cabb6d7-2c88-50b5-8108-2d03ff2b15da"), "Shadowmoon Valley" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("998fb25f-b61d-5d90-8376-30f05f87e126"), new Guid("0cabb6d7-2c88-50b5-8108-2d03ff2b15da"), "Terokkar Forest" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("99f11b6f-25cc-5ead-a37c-d7f726ebb267"), new Guid("0cabb6d7-2c88-50b5-8108-2d03ff2b15da"), "Blades Edge Mountains" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("a64bbf36-eff5-51ec-8e40-b9149f045014"), new Guid("4eab05b5-c6fa-5f31-9ae0-d9901ebdc673"), "Zandalar" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("a8505ad1-6dfb-5a1e-9257-caf1c23f60ca"), new Guid("4eab05b5-c6fa-5f31-9ae0-d9901ebdc673"), "Kalimdor" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("abf423e0-0bf8-5a9c-8d34-a7dfc394e973"), new Guid("0cabb6d7-2c88-50b5-8108-2d03ff2b15da"), "Zangarmarsh" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("add2811d-613a-53fb-b617-7b00dfd86720"), new Guid("0cabb6d7-2c88-50b5-8108-2d03ff2b15da"), "Nagrand" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("b49777bc-7c0f-5806-8672-5343d90ff9fa"), new Guid("4eab05b5-c6fa-5f31-9ae0-d9901ebdc673"), "Kul Tiras" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("cf57e1ef-fcef-5a8e-b358-e39ea91cd9a0"), new Guid("4eab05b5-c6fa-5f31-9ae0-d9901ebdc673"), "Northrend" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("e1d570a6-b267-5d72-bf50-6435a4f22db5"), new Guid("4eab05b5-c6fa-5f31-9ae0-d9901ebdc673"), "Eastern Kingdoms" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("f9c38568-4490-5f86-90f3-f7fe740ba3fd"), new Guid("4eab05b5-c6fa-5f31-9ae0-d9901ebdc673"), "Broken Isles" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("53e050b1-cd32-5510-a813-dc3acf0e7e4c"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("643b0680-fe42-581a-9b29-bfac979992a7"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("6ac4b2fa-effc-5535-9e70-2cad13fadf1c"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("9463c17b-e669-5941-8d71-0d5e959b6d13"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("998fb25f-b61d-5d90-8376-30f05f87e126"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("99f11b6f-25cc-5ead-a37c-d7f726ebb267"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("a64bbf36-eff5-51ec-8e40-b9149f045014"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("a8505ad1-6dfb-5a1e-9257-caf1c23f60ca"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("abf423e0-0bf8-5a9c-8d34-a7dfc394e973"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("add2811d-613a-53fb-b617-7b00dfd86720"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("b49777bc-7c0f-5806-8672-5343d90ff9fa"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("cf57e1ef-fcef-5a8e-b358-e39ea91cd9a0"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("e1d570a6-b267-5d72-bf50-6435a4f22db5"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("f9c38568-4490-5f86-90f3-f7fe740ba3fd"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0cabb6d7-2c88-50b5-8108-2d03ff2b15da"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("4eab05b5-c6fa-5f31-9ae0-d9901ebdc673"));
        }
    }
}
