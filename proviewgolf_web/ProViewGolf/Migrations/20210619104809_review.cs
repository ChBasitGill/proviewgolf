using Microsoft.EntityFrameworkCore.Migrations;

namespace ProViewGolf.Migrations
{
    public partial class review : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grip = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Alignment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Flexibility = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fitness = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BallPosition = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HandPosition = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TakeAway = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HeadMovement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GripPressure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FollowThrough = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Realease = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Folding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishPosition = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PuttingTechnique = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Dipping = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightTransfer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Concentration = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MentalStrength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Etiquette = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GolfRules = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CourseManagement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaceOfGame = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ControlBall = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlayingPunch = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BackspinControl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SwippingTheBall = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Overswinging = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Looping = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReverseWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bowing = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Blocking = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lifting = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LawBallFight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StudentRefId = table.Column<long>(type: "bigint", nullable: false),
                    StudentUserId = table.Column<long>(type: "bigint", nullable: true),
                    ProRefId = table.Column<long>(type: "bigint", nullable: false),
                    ProUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_ProUserId",
                        column: x => x.ProUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProUserId",
                table: "Reviews",
                column: "ProUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StudentUserId",
                table: "Reviews",
                column: "StudentUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
