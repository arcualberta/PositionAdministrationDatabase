﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PD.Data;

namespace PD.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190311210033_AddedIsProjectionToSalaryScale")]
    partial class AddedIsProjectionToSalaryScale
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PD.Models.AuditRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditType");

                    b.Property<bool>("IsProjectionLog");

                    b.Property<string>("Message");

                    b.Property<int?>("PositionAssignmentId");

                    b.Property<string>("SalaryCycle");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PositionAssignmentId");

                    b.ToTable("AuditTrail");
                });

            modelBuilder.Entity("PD.Models.ChartField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("ChartFields");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ChartField");
                });

            modelBuilder.Entity("PD.Models.ChartField2ChartStringJoin", b =>
                {
                    b.Property<int>("ChartFieldId");

                    b.Property<int>("ChartStringId");

                    b.HasKey("ChartFieldId", "ChartStringId");

                    b.HasIndex("ChartStringId");

                    b.ToTable("ChartField2ChartStringJoins");
                });

            modelBuilder.Entity("PD.Models.ChartString", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ComboCode");

                    b.Property<string>("SpeedCode");

                    b.HasKey("Id");

                    b.ToTable("ChartStrings");
                });

            modelBuilder.Entity("PD.Models.Compensations.Compensation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsProjection");

                    b.Property<string>("Notes");

                    b.Property<int>("PositionAssignmentId");

                    b.Property<DateTime>("StartDate");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(19, 5)");

                    b.HasKey("Id");

                    b.HasIndex("PositionAssignmentId");

                    b.ToTable("Compensations");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Compensation");
                });

            modelBuilder.Entity("PD.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PD.Models.HangFireJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("JobId");

                    b.Property<string>("JobKey");

                    b.HasKey("Id");

                    b.ToTable("HangFireJobs");
                });

            modelBuilder.Entity("PD.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("Hash");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PD.Models.PositionAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChartStringId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("PositionId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<decimal>("ValuePercentage")
                        .HasColumnType("decimal(19, 5)");

                    b.HasKey("Id");

                    b.HasIndex("ChartStringId");

                    b.HasIndex("PositionId");

                    b.ToTable("PositionAccounts");
                });

            modelBuilder.Entity("PD.Models.PositionAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate");

                    b.Property<int?>("PersonId");

                    b.Property<int?>("PositionId");

                    b.Property<int?>("PredecessorId");

                    b.Property<int>("SalaryCycleStartDay");

                    b.Property<int>("SalaryCycleStartMonth");

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("Status");

                    b.Property<int?>("SucccessorId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PositionId");

                    b.HasIndex("PredecessorId")
                        .IsUnique()
                        .HasFilter("[PredecessorId] IS NOT NULL");

                    b.ToTable("PositionAssignments");
                });

            modelBuilder.Entity("PD.Models.Positions.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractType");

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Number");

                    b.Property<int?>("PrimaryDepartmentId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Title");

                    b.Property<decimal>("Workload")
                        .HasColumnType("decimal(19, 5)");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryDepartmentId");

                    b.ToTable("Positions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Position");
                });

            modelBuilder.Entity("PD.Models.PromotionScheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrentTitle");

                    b.Property<string>("PromotedTitle");

                    b.HasKey("Id");

                    b.ToTable("PromotionSchemes");
                });

            modelBuilder.Entity("PD.Models.SalaryScales.SalaryScale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("ContractSettlement")
                        .HasColumnType("decimal(19, 5)");

                    b.Property<decimal>("DefaultMeritDecision")
                        .HasColumnType("decimal(19, 5)");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("IsProjection");

                    b.Property<decimal>("Maximum")
                        .HasColumnType("decimal(19, 5)");

                    b.Property<decimal>("Minimum")
                        .HasColumnType("decimal(19, 5)");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("StartDate");

                    b.Property<decimal>("StepValue")
                        .HasColumnType("decimal(19, 5)");

                    b.HasKey("Id");

                    b.ToTable("SalaryScales");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SalaryScale");
                });

            modelBuilder.Entity("PD.Models.Speedcode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Speedcodes");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Account", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");

                    b.HasDiscriminator().HasValue("Account");
                });

            modelBuilder.Entity("PD.Models.ChartFields.BusinessUnit", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");

                    b.HasDiscriminator().HasValue("BusinessUnit");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Class", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");

                    b.HasDiscriminator().HasValue("Class");
                });

            modelBuilder.Entity("PD.Models.ChartFields.DeptID", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");

                    b.Property<int>("DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.HasDiscriminator().HasValue("DeptID");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Fund", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");

                    b.HasDiscriminator().HasValue("Fund");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Program", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");

                    b.HasDiscriminator().HasValue("Program");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Project", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");

                    b.HasDiscriminator().HasValue("Project");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Sponsor", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");

                    b.HasDiscriminator().HasValue("Sponsor");
                });

            modelBuilder.Entity("PD.Models.Compensations.Adjustment", b =>
                {
                    b.HasBaseType("PD.Models.Compensations.Compensation");

                    b.Property<bool>("IsBaseSalaryComponent");

                    b.Property<string>("Name");

                    b.HasDiscriminator().HasValue("Adjustment");
                });

            modelBuilder.Entity("PD.Models.Compensations.ContractSettlement", b =>
                {
                    b.HasBaseType("PD.Models.Compensations.Compensation");

                    b.HasDiscriminator().HasValue("ContractSettlement");
                });

            modelBuilder.Entity("PD.Models.Compensations.Merit", b =>
                {
                    b.HasBaseType("PD.Models.Compensations.Compensation");

                    b.Property<bool>("IsPromoted");

                    b.Property<decimal>("MeritDecision")
                        .HasColumnType("decimal(19, 5)");

                    b.HasDiscriminator().HasValue("Merit");
                });

            modelBuilder.Entity("PD.Models.Compensations.Salary", b =>
                {
                    b.HasBaseType("PD.Models.Compensations.Compensation");

                    b.Property<bool>("IsMaxed");

                    b.HasDiscriminator().HasValue("Salary");
                });

            modelBuilder.Entity("PD.Models.Positions.Apo", b =>
                {
                    b.HasBaseType("PD.Models.Positions.Position");

                    b.HasDiscriminator().HasValue("Apo");
                });

            modelBuilder.Entity("PD.Models.Positions.Faculty", b =>
                {
                    b.HasBaseType("PD.Models.Positions.Position");

                    b.HasDiscriminator().HasValue("Faculty");
                });

            modelBuilder.Entity("PD.Models.Positions.Nasa", b =>
                {
                    b.HasBaseType("PD.Models.Positions.Position");

                    b.HasDiscriminator().HasValue("Nasa");
                });

            modelBuilder.Entity("PD.Models.SalaryScales.APOSalaryScale", b =>
                {
                    b.HasBaseType("PD.Models.SalaryScales.SalaryScale");

                    b.HasDiscriminator().HasValue("APOSalaryScale");
                });

            modelBuilder.Entity("PD.Models.SalaryScales.FacultySalaryScale", b =>
                {
                    b.HasBaseType("PD.Models.SalaryScales.SalaryScale");

                    b.HasDiscriminator().HasValue("FacultySalaryScale");
                });

            modelBuilder.Entity("PD.Models.SalaryScales.NASASalaryScale", b =>
                {
                    b.HasBaseType("PD.Models.SalaryScales.SalaryScale");

                    b.HasDiscriminator().HasValue("NASASalaryScale");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PD.Models.AuditRecord", b =>
                {
                    b.HasOne("PD.Models.PositionAssignment")
                        .WithMany("AuditTrail")
                        .HasForeignKey("PositionAssignmentId");
                });

            modelBuilder.Entity("PD.Models.ChartField2ChartStringJoin", b =>
                {
                    b.HasOne("PD.Models.ChartField", "ChartField")
                        .WithMany("ChartStrings")
                        .HasForeignKey("ChartFieldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PD.Models.ChartString", "ChartString")
                        .WithMany("ChartFields")
                        .HasForeignKey("ChartStringId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PD.Models.Compensations.Compensation", b =>
                {
                    b.HasOne("PD.Models.PositionAssignment", "PositionAssignment")
                        .WithMany("Compensations")
                        .HasForeignKey("PositionAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PD.Models.PositionAccount", b =>
                {
                    b.HasOne("PD.Models.ChartString", "ChartString")
                        .WithMany("PositionAccounts")
                        .HasForeignKey("ChartStringId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PD.Models.Positions.Position", "Position")
                        .WithMany("PositionAccounts")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PD.Models.PositionAssignment", b =>
                {
                    b.HasOne("PD.Models.Person", "Person")
                        .WithMany("PositionAssignments")
                        .HasForeignKey("PersonId");

                    b.HasOne("PD.Models.Positions.Position", "Position")
                        .WithMany("PositionAssignments")
                        .HasForeignKey("PositionId");

                    b.HasOne("PD.Models.PositionAssignment", "Predecessor")
                        .WithOne("Succcessor")
                        .HasForeignKey("PD.Models.PositionAssignment", "PredecessorId");
                });

            modelBuilder.Entity("PD.Models.Positions.Position", b =>
                {
                    b.HasOne("PD.Models.Department", "PrimaryDepartment")
                        .WithMany()
                        .HasForeignKey("PrimaryDepartmentId");
                });

            modelBuilder.Entity("PD.Models.ChartFields.DeptID", b =>
                {
                    b.HasOne("PD.Models.Department", "Department")
                        .WithMany("DeptIDs")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
