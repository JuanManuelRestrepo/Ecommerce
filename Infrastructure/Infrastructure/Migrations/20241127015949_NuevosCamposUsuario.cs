using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NuevosCamposUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7599b542-dbb0-4a96-a30d-1badc80034ec"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d5f74dc0-1f75-4ee9-9c2d-60ae1714355b"));

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Telefono",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RolName" },
                values: new object[,]
                {
                    { new Guid("a4d57d79-9158-47ea-b303-eac5ac8733c7"), "Usuario" },
                    { new Guid("e78202bc-25e6-437b-89ad-0775cffa2196"), "Administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a4d57d79-9158-47ea-b303-eac5ac8733c7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e78202bc-25e6-437b-89ad-0775cffa2196"));

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Usuarios");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RolName" },
                values: new object[,]
                {
                    { new Guid("7599b542-dbb0-4a96-a30d-1badc80034ec"), "Usuario" },
                    { new Guid("d5f74dc0-1f75-4ee9-9c2d-60ae1714355b"), "Administrador" }
                });
        }
    }
}
