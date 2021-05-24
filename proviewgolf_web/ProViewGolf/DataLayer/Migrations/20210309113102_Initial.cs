using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ProViewGolf.DataLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    InvitationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn)
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
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Profile_FirstName = table.Column<string>(type: "text", nullable: true),
                    Profile_LastName = table.Column<string>(type: "text", nullable: true),
                    Profile_Phone = table.Column<string>(type: "text", nullable: true),
                    Profile_Gender = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Profile_Age = table.Column<int>(type: "int", nullable: true),
                    Profile_Hcp = table.Column<int>(type: "int", nullable: true),
                    Profile_DistanceUnit = table.Column<int>(type: "int", nullable: true),
                    Profile_SpeedUnit = table.Column<int>(type: "int", nullable: true),
                    AccountVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    VerificationToken = table.Column<string>(type: "text", nullable: true),
                    VerificationTokenExpiry = table.Column<DateTime>(type: "datetime", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    ProViewHcp = table.Column<int>(type: "int", nullable: true),
                    ProViewLevel = table.Column<int>(type: "int", nullable: true),
                    ProRefId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Users_ProRefId",
                        column: x => x.ProRefId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubPractices",
                columns: table => new
                {
                    ClubPracticeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsWithPro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Ground = table.Column<int>(type: "int", nullable: false),
                    Club = table.Column<int>(type: "int", nullable: false),
                    AvgDistance = table.Column<float>(type: "float", nullable: false),
                    ClubHeadSpeed = table.Column<float>(type: "float", nullable: false),
                    SpinRate = table.Column<float>(type: "float", nullable: false),
                    Apex = table.Column<float>(type: "float", nullable: false),
                    BallsAmount = table.Column<float>(type: "float", nullable: false),
                    Rating = table.Column<float>(type: "float", nullable: false),
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
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Club = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    Shaft = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    ClubLoft = table.Column<int>(type: "int", nullable: false),
                    Grip = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<string>(type: "text", nullable: true),
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
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    GameType = table.Column<int>(type: "int", nullable: false),
                    WarmupTime = table.Column<int>(type: "int", nullable: false),
                    DriverPeaces = table.Column<int>(type: "int", nullable: false),
                    IronPeaces = table.Column<int>(type: "int", nullable: false),
                    ChipPeaces = table.Column<int>(type: "int", nullable: false),
                    SandPeaces = table.Column<int>(type: "int", nullable: false),
                    PuttPeaces = table.Column<int>(type: "int", nullable: false),
                    GolfCourse = table.Column<string>(type: "text", nullable: true),
                    ExactHcp = table.Column<float>(type: "float", nullable: false),
                    PlayingHcp = table.Column<int>(type: "int", nullable: false),
                    Nervous = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FlightPartnersRating = table.Column<float>(type: "float", nullable: false),
                    DriversRating = table.Column<float>(type: "float", nullable: false),
                    DriversLeft = table.Column<int>(type: "int", nullable: false),
                    DriversCenter = table.Column<int>(type: "int", nullable: false),
                    DriversRight = table.Column<int>(type: "int", nullable: false),
                    IronsRating = table.Column<float>(type: "float", nullable: false),
                    IronsLeft = table.Column<int>(type: "int", nullable: false),
                    IronsCenter = table.Column<int>(type: "int", nullable: false),
                    IronsRight = table.Column<int>(type: "int", nullable: false),
                    WoodsRating = table.Column<float>(type: "float", nullable: false),
                    WoodsLeft = table.Column<int>(type: "int", nullable: false),
                    WoodsCenter = table.Column<int>(type: "int", nullable: false),
                    WoodsRight = table.Column<int>(type: "int", nullable: false),
                    ShortIronGameRating = table.Column<float>(type: "float", nullable: false),
                    BunkerShortsRating = table.Column<float>(type: "float", nullable: false),
                    PuttingStrokes = table.Column<int>(type: "int", nullable: false),
                    GreenSpeedRating = table.Column<float>(type: "float", nullable: false),
                    StableFordPoints = table.Column<int>(type: "int", nullable: false),
                    Strokes = table.Column<int>(type: "int", nullable: false),
                    NewHcp = table.Column<float>(type: "float", nullable: false),
                    Walking = table.Column<bool>(type: "tinyint(1)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTime>(type: "datetime", nullable: false),
                    End = table.Column<DateTime>(type: "datetime", nullable: false),
                    StudentRefId = table.Column<long>(type: "bigint", nullable: false),
                    ProRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_ProRefId",
                        column: x => x.ProRefId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_StudentRefId",
                        column: x => x.StudentRefId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShotPractices",
                columns: table => new
                {
                    ShotPracticeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsWithPro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ShotCategory = table.Column<int>(type: "int", nullable: false),
                    ShotType = table.Column<int>(type: "int", nullable: false),
                    Shots = table.Column<int>(type: "int", nullable: false),
                    GoodShots = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<float>(type: "float", nullable: false),
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
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false),
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
                name: "IX_Instructors_ProRefId",
                table: "Instructors",
                column: "ProRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_StudentRefId",
                table: "Instructors",
                column: "StudentRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ProRefId",
                table: "Sessions",
                column: "ProRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_StudentRefId",
                table: "Sessions",
                column: "StudentRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ShotPractices_StudentId",
                table: "ShotPractices",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_StudentId",
                table: "Skills",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProRefId",
                table: "Users",
                column: "ProRefId");
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
                name: "Instructors");

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
