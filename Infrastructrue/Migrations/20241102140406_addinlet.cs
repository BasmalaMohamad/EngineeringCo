using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructrue.Migrations
{
    /// <inheritdoc />
    public partial class addinlet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InletSizeInch",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "OutletSizeMM",
                table: "Products",
                newName: "OutletSize");

            migrationBuilder.RenameColumn(
                name: "OutletSizeInch",
                table: "Products",
                newName: "InletSize");

            migrationBuilder.RenameColumn(
                name: "InletSizeMM",
                table: "Products",
                newName: "AirInletSize");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutletSize",
                table: "Products",
                newName: "OutletSizeMM");

            migrationBuilder.RenameColumn(
                name: "InletSize",
                table: "Products",
                newName: "OutletSizeInch");

            migrationBuilder.RenameColumn(
                name: "AirInletSize",
                table: "Products",
                newName: "InletSizeMM");

            migrationBuilder.AddColumn<float>(
                name: "InletSizeInch",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
