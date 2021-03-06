// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProViewGolf.DataLayer;

namespace ProViewGolf.DataLayer.Migrations
{
    [DbContext(typeof(ProGolfContext))]
    partial class ProGolfContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.ClubPractice", b =>
                {
                    b.Property<long>("ClubPracticeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Apex")
                        .HasColumnType("real");

                    b.Property<float>("AvgDistance")
                        .HasColumnType("real");

                    b.Property<float>("BallsAmount")
                        .HasColumnType("real");

                    b.Property<int>("Club")
                        .HasColumnType("int");

                    b.Property<float>("ClubHeadSpeed")
                        .HasColumnType("real");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Ground")
                        .HasColumnType("int");

                    b.Property<bool>("IsWithPro")
                        .HasColumnType("bit");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<float>("SpinRate")
                        .HasColumnType("real");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("ClubPracticeId");

                    b.HasIndex("StudentId");

                    b.ToTable("ClubPractices");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Equipment", b =>
                {
                    b.Property<long>("EquipmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Club")
                        .HasColumnType("int");

                    b.Property<int>("ClubLoft")
                        .HasColumnType("int");

                    b.Property<string>("Grip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pairs")
                        .HasColumnType("int");

                    b.Property<int>("Pieces")
                        .HasColumnType("int");

                    b.Property<string>("Shaft")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("EquipmentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Game", b =>
                {
                    b.Property<long>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BunkerShortsRating")
                        .HasColumnType("real");

                    b.Property<int>("ChipPeaces")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DistanceWalked")
                        .HasColumnType("int");

                    b.Property<int>("DriverPeaces")
                        .HasColumnType("int");

                    b.Property<int>("DriversCenter")
                        .HasColumnType("int");

                    b.Property<int>("DriversLeft")
                        .HasColumnType("int");

                    b.Property<float>("DriversRating")
                        .HasColumnType("real");

                    b.Property<int>("DriversRight")
                        .HasColumnType("int");

                    b.Property<float>("ExactHcp")
                        .HasColumnType("real");

                    b.Property<float>("FlightPartnersRating")
                        .HasColumnType("real");

                    b.Property<int>("GameDuration")
                        .HasColumnType("int");

                    b.Property<int>("GameType")
                        .HasColumnType("int");

                    b.Property<string>("GolfCourse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("GreenSpeedRating")
                        .HasColumnType("real");

                    b.Property<int>("Holes")
                        .HasColumnType("int");

                    b.Property<int>("IronPeaces")
                        .HasColumnType("int");

                    b.Property<int>("IronsCenter")
                        .HasColumnType("int");

                    b.Property<int>("IronsLeft")
                        .HasColumnType("int");

                    b.Property<float>("IronsRating")
                        .HasColumnType("real");

                    b.Property<int>("IronsRight")
                        .HasColumnType("int");

                    b.Property<bool>("Nervous")
                        .HasColumnType("bit");

                    b.Property<float>("NewHcp")
                        .HasColumnType("real");

                    b.Property<int>("PlayingHcp")
                        .HasColumnType("int");

                    b.Property<int>("PuttPeaces")
                        .HasColumnType("int");

                    b.Property<int>("PuttingStrokes")
                        .HasColumnType("int");

                    b.Property<int>("SandPeaces")
                        .HasColumnType("int");

                    b.Property<float>("ShortIronGameRating")
                        .HasColumnType("real");

                    b.Property<int>("StableFordPoints")
                        .HasColumnType("int");

                    b.Property<int>("Strokes")
                        .HasColumnType("int");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Walking")
                        .HasColumnType("bit");

                    b.Property<int>("WarmupTime")
                        .HasColumnType("int");

                    b.Property<int>("WoodsCenter")
                        .HasColumnType("int");

                    b.Property<int>("WoodsLeft")
                        .HasColumnType("int");

                    b.Property<float>("WoodsRating")
                        .HasColumnType("real");

                    b.Property<int>("WoodsRight")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.HasIndex("StudentId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Invitation", b =>
                {
                    b.Property<long>("InvitationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstructorEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StudentEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InvitationId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Session", b =>
                {
                    b.Property<long>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<long>("ProRefId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<long>("StudentRefId")
                        .HasColumnType("bigint");

                    b.HasKey("SessionId");

                    b.HasIndex("ProRefId");

                    b.HasIndex("StudentRefId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.ShotPractice", b =>
                {
                    b.Property<long>("ShotPracticeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GoodShots")
                        .HasColumnType("int");

                    b.Property<bool>("IsWithPro")
                        .HasColumnType("bit");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("ShotCategory")
                        .HasColumnType("int");

                    b.Property<int>("ShotType")
                        .HasColumnType("int");

                    b.Property<int>("Shots")
                        .HasColumnType("int");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("ShotPracticeId");

                    b.HasIndex("StudentId");

                    b.ToTable("ShotPractices");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Skill", b =>
                {
                    b.Property<long>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlignmentDrill")
                        .HasColumnType("int");

                    b.Property<int>("CourseManagement")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FitnessSessionCore")
                        .HasColumnType("int");

                    b.Property<int>("FitnessSessionLowerBody")
                        .HasColumnType("int");

                    b.Property<int>("FitnessSessionUpperBody")
                        .HasColumnType("int");

                    b.Property<int>("GreenReading")
                        .HasColumnType("int");

                    b.Property<int>("MentalTraining")
                        .HasColumnType("int");

                    b.Property<int>("RulesQuiz")
                        .HasColumnType("int");

                    b.Property<int>("Stretching")
                        .HasColumnType("int");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<int>("VideoSwingAnalysis")
                        .HasColumnType("int");

                    b.Property<int>("_18HolesPlayedWithGolfCar")
                        .HasColumnType("int");

                    b.Property<int>("_18HolesWalk")
                        .HasColumnType("int");

                    b.Property<int>("_9HolesWalk")
                        .HasColumnType("int");

                    b.HasKey("SkillId");

                    b.HasIndex("StudentId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AccountVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VerificationTokenExpiry")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Pro", b =>
                {
                    b.HasBaseType("ProViewGolf.Core.Dbo.Entities.User");

                    b.HasDiscriminator().HasValue("Pro");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Student", b =>
                {
                    b.HasBaseType("ProViewGolf.Core.Dbo.Entities.User");

                    b.Property<long>("ProRefId")
                        .HasColumnType("bigint");

                    b.Property<int>("ProViewHcp")
                        .HasColumnType("int");

                    b.Property<int>("ProViewLevel")
                        .HasColumnType("int");

                    b.HasIndex("ProRefId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.ClubPractice", b =>
                {
                    b.HasOne("ProViewGolf.Core.Dbo.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Equipment", b =>
                {
                    b.HasOne("ProViewGolf.Core.Dbo.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Game", b =>
                {
                    b.HasOne("ProViewGolf.Core.Dbo.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Session", b =>
                {
                    b.HasOne("ProViewGolf.Core.Dbo.Entities.Pro", "Pro")
                        .WithMany()
                        .HasForeignKey("ProRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProViewGolf.Core.Dbo.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pro");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.ShotPractice", b =>
                {
                    b.HasOne("ProViewGolf.Core.Dbo.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Skill", b =>
                {
                    b.HasOne("ProViewGolf.Core.Dbo.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.User", b =>
                {
                    b.OwnsOne("ProViewGolf.Core.Dbo.Entities.Profile", "Profile", b1 =>
                        {
                            b1.Property<long>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Age")
                                .HasColumnType("int");

                            b1.Property<int>("DistanceUnit")
                                .HasColumnType("int");

                            b1.Property<string>("FirstName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("Gender")
                                .HasColumnType("bit");

                            b1.Property<int>("Hcp")
                                .HasColumnType("int");

                            b1.Property<string>("HealthLimitation")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LastName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("SpeedUnit")
                                .HasColumnType("int");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Student", b =>
                {
                    b.HasOne("ProViewGolf.Core.Dbo.Entities.Pro", "Pro")
                        .WithMany("Students")
                        .HasForeignKey("ProRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pro");
                });

            modelBuilder.Entity("ProViewGolf.Core.Dbo.Entities.Pro", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
