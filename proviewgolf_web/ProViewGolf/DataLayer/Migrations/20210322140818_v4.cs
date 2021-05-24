using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ProViewGolf.DataLayer.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instructors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    InvitationCode = table.Column<string>(type: "text", nullable: true),
                    InvitationStatus = table.Column<int>(type: "int", nullable: false),
                    ProRefId = table.Column<long>(type: "bigint", nullable: false),
                    StudentRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                    table.ForeignKey(
                        name: "FK_Instructors_Users_ProRefId",
                        column: x => x.ProRefId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructors_Users_StudentRefId",
                        column: x => x.StudentRefId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_ProRefId",
                table: "Instructors",
                column: "ProRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_StudentRefId",
                table: "Instructors",
                column: "StudentRefId");
        }
    }
}
