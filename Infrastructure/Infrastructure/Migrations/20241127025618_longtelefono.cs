using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class longtelefono : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a4d57d79-9158-47ea-b303-eac5ac8733c7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e78202bc-25e6-437b-89ad-0775cffa2196"));

            migrationBuilder.AlterColumn<long>(
                name: "Telefono",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RolName" },
                values: new object[,]
                {
                    { new Guid("7d46913e-7678-4541-bd3b-b1a1882058aa"), "Usuario" },
                    { new Guid("980c093b-a0a8-46f2-8d98-437aa1ec2b49"), "Administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d46913e-7678-4541-bd3b-b1a1882058aa"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("980c093b-a0a8-46f2-8d98-437aa1ec2b49"));

            migrationBuilder.AlterColumn<int>(
                name: "Telefono",
                table: "Usuarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RolName" },
                values: new object[,]
                {
                    { new Guid("a4d57d79-9158-47ea-b303-eac5ac8733c7"), "Usuario" },
                    { new Guid("e78202bc-25e6-437b-89ad-0775cffa2196"), "Administrador" }
                });
        }
    }
}
