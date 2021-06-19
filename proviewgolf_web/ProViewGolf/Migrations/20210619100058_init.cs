using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProViewGolf.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    InvitationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructorEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.InvitationId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile_Gender = table.Column<bool>(type: "bit", nullable: true),
                    Profile_Age = table.Column<int>(type: "int", nullable: true),
                    Profile_Hcp = table.Column<int>(type: "int", nullable: true),
                    Profile_HealthLimitation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile_DistanceUnit = table.Column<int>(type: "int", nullable: true),
                    Profile_SpeedUnit = table.Column<int>(type: "int", nullable: true),
                    AccountVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProViewHcp = table.Column<int>(type: "int", nullable: true),
                    ProViewLevel = table.Column<int>(type: "int", nullable: true),
                    ProRefId = table.Column<long>(type: "bigint", nullable: true),
                    ProUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Users_ProUserId",
                        column: x => x.ProUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClubPractices",
                columns: table => new
                {
                    ClubPracticeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsWithPro = table.Column<bool>(type: "bit", nullable: false),
                    Ground = table.Column<int>(type: "int", nullable: false),
                    Club = table.Column<int>(type: "int", nullable: false),
                    AvgDistance = table.Column<float>(type: "real", nullable: false),
                    ClubHeadSpeed = table.Column<float>(type: "real", nullable: false),
                    SpinRate = table.Column<float>(type: "real", nullable: false),
                    Apex = table.Column<float>(type: "real", nullable: false),
                    BallsAmount = table.Column<float>(type: "real", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubPractices", x => x.ClubPracticeId);
                    table.ForeignKey(
                        name: "FK_ClubPractices_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    EquipmentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Club = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shaft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClubLoft = table.Column<int>(type: "int", nullable: false),
                    Grip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pairs = table.Column<int>(type: "int", nullable: false),
                    Pieces = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_Equipments_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameType = table.Column<int>(type: "int", nullable: false),
                    WarmupTime = table.Column<int>(type: "int", nullable: false),
                    DriverPeaces = table.Column<int>(type: "int", nullable: false),
                    IronPeaces = table.Column<int>(type: "int", nullable: false),
                    ChipPeaces = table.Column<int>(type: "int", nullable: false),
                    SandPeaces = table.Column<int>(type: "int", nullable: false),
                    PuttPeaces = table.Column<int>(type: "int", nullable: false),
                    GolfCourse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExactHcp = table.Column<double>(type: "float", nullable: false),
                    PlayingHcp = table.Column<int>(type: "int", nullable: false),
                    Nervous = table.Column<bool>(type: "bit", nullable: false),
                    FlightPartnersRating = table.Column<double>(type: "float", nullable: false),
                    DriversRating = table.Column<double>(type: "float", nullable: false),
                    DriversLeft = table.Column<int>(type: "int", nullable: false),
                    DriversCenter = table.Column<int>(type: "int", nullable: false),
                    DriversRight = table.Column<int>(type: "int", nullable: false),
                    IronsRating = table.Column<double>(type: "float", nullable: false),
                    IronsLeft = table.Column<int>(type: "int", nullable: false),
                    IronsCenter = table.Column<int>(type: "int", nullable: false),
                    IronsRight = table.Column<int>(type: "int", nullable: false),
                    WoodsRating = table.Column<double>(type: "float", nullable: false),
                    WoodsLeft = table.Column<int>(type: "int", nullable: false),
                    WoodsCenter = table.Column<int>(type: "int", nullable: false),
                    WoodsRight = table.Column<int>(type: "int", nullable: false),
                    ShortIronGameRating = table.Column<double>(type: "float", nullable: false),
                    BunkerShortsRating = table.Column<double>(type: "float", nullable: false),
                    PuttingStrokes = table.Column<int>(type: "int", nullable: false),
                    GreenSpeedRating = table.Column<double>(type: "float", nullable: false),
                    StableFordPoints = table.Column<int>(type: "int", nullable: false),
                    Strokes = table.Column<int>(type: "int", nullable: false),
                    NewHcp = table.Column<double>(type: "float", nullable: false),
                    Walking = table.Column<bool>(type: "bit", nullable: false),
                    DistanceWalked = table.Column<int>(type: "int", nullable: false),
                    GameDuration = table.Column<int>(type: "int", nullable: false),
                    Holes = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentRefId = table.Column<long>(type: "bigint", nullable: false),
                    StudentUserId = table.Column<long>(type: "bigint", nullable: true),
                    ProRefId = table.Column<long>(type: "bigint", nullable: false),
                    ProUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_ProUserId",
                        column: x => x.ProUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShotPractices",
                columns: table => new
                {
                    ShotPracticeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsWithPro = table.Column<bool>(type: "bit", nullable: false),
                    ShotCategory = table.Column<int>(type: "int", nullable: false),
                    ShotType = table.Column<int>(type: "int", nullable: false),
                    Shots = table.Column<int>(type: "int", nullable: false),
                    GoodShots = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShotPractices", x => x.ShotPracticeId);
                    table.ForeignKey(
                        name: "FK_ShotPractices_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stretching = table.Column<int>(type: "int", nullable: false),
                    FitnessSessionLowerBody = table.Column<int>(type: "int", nullable: false),
                    FitnessSessionUpperBody = table.Column<int>(type: "int", nullable: false),
                    FitnessSessionCore = table.Column<int>(type: "int", nullable: false),
                    MentalTraining = table.Column<int>(type: "int", nullable: false),
                    AlignmentDrill = table.Column<int>(type: "int", nullable: false),
                    GreenReading = table.Column<int>(type: "int", nullable: false),
                    CourseManagement = table.Column<int>(type: "int", nullable: false),
                    RulesQuiz = table.Column<int>(type: "int", nullable: false),
                    VideoSwingAnalysis = table.Column<int>(type: "int", nullable: false),
                    _18HolesWalk = table.Column<int>(type: "int", nullable: false),
                    _9HolesWalk = table.Column<int>(type: "int", nullable: false),
                    _18HolesPlayedWithGolfCar = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skills_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubPractices_StudentId",
                table: "ClubPractices",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_StudentId",
                table: "Equipments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_StudentId",
                table: "Games",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ProUserId",
                table: "Sessions",
                column: "ProUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_StudentUserId",
                table: "Sessions",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShotPractices_StudentId",
                table: "ShotPractices",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_StudentId",
                table: "Skills",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProUserId",
                table: "Users",
                column: "ProUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubPractices");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "ShotPractices");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
