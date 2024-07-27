using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateModelsNullabilty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceSpecifications_DeviceSpecification",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Sponsorships",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "INFO",
                table: "DeviceSpecifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceSpecification",
                table: "Devices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryDescription",
                table: "DeviceCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceSpecifications_DeviceSpecification",
                table: "Devices",
                column: "DeviceSpecification",
                principalTable: "DeviceSpecifications",
                principalColumn: "SpecificationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceSpecifications_DeviceSpecification",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Sponsorships",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "INFO",
                table: "DeviceSpecifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeviceSpecification",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryDescription",
                table: "DeviceCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceSpecifications_DeviceSpecification",
                table: "Devices",
                column: "DeviceSpecification",
                principalTable: "DeviceSpecifications",
                principalColumn: "SpecificationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
