using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDOP.Migrations
{
    /// <inheritdoc />
    public partial class intailization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPage",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderBy",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Term",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPages",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pageSize",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPage",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OrderBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Term",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TotalPages",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "pageSize",
                table: "Employees");
        }
    }
}
