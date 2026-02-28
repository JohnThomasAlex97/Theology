using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Th_Dpt.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashToMasterUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "MasterUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TTTID",
                table: "MasterUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "MasterUsers");

            migrationBuilder.DropColumn(
                name: "TTTID",
                table: "MasterUsers");
        }
    }
}
