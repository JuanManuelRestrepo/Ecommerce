using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7c2727b4-1dab-414e-9701-6649ebc7a1ad"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b78abcef-be7b-4b2c-8635-e712dc69848d"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RolName" },
                values: new object[,]
                {
                    { new Guid("54db6fb5-ab60-40f2-b6af-08adc11f9249"), "Usuario" },
                    { new Guid("8ef49426-01eb-4777-a8e3-459ecc4bee51"), "Administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("54db6fb5-ab60-40f2-b6af-08adc11f9249"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8ef49426-01eb-4777-a8e3-459ecc4bee51"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RolName" },
                values: new object[,]
                {
                    { new Guid("7c2727b4-1dab-414e-9701-6649ebc7a1ad"), "Administrador" },
                    { new Guid("b78abcef-be7b-4b2c-8635-e712dc69848d"), "Usuario" }
                });
        }
    }
}
