using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AioCore.Web.Migrations
{
    public partial class InitialSettingsDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Settings");

            migrationBuilder.CreateTable(
                name: "Configs",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gateway = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Netmask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dns1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dns2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaseDuration = table.Column<int>(type: "int", nullable: false),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Device = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bootloader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FingerPrint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hardware = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimestampShort = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlatformAccounts",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlatformType = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformAccounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Id",
                schema: "Settings",
                table: "Devices",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Timestamp",
                schema: "Settings",
                table: "Devices",
                column: "Timestamp",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configs",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "Devices",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "Machines",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "PlatformAccounts",
                schema: "Settings");
        }
    }
}
