using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditDeviceSpecification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceSpecifications_DeviceSpecification",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceSpecification",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceSpecification",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "DeviceINFO",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceINFO",
                table: "Devices");

            migrationBuilder.AddColumn<int>(
                name: "DeviceSpecification",
                table: "Devices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceSpecifications",
                columns: table => new
                {
                    SpecificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INFO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceSpecifications", x => x.SpecificationID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceSpecification",
                table: "Devices",
                column: "DeviceSpecification");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceSpecifications_DeviceSpecification",
                table: "Devices",
                column: "DeviceSpecification",
                principalTable: "DeviceSpecifications",
                principalColumn: "SpecificationID");
        }
    }
}
