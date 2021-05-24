using Microsoft.EntityFrameworkCore.Migrations;

namespace ProViewGolf.DataLayer.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profile_HealthLimitation",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile_HealthLimitation",
                table: "Users");
        }
    }
}
