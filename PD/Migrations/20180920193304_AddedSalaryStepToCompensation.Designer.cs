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
    [Migration("20180920193304_AddedSalaryStepToCompensation")]
    partial class AddedSalaryStepToCompensation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

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
                        .ValueGeneratedOnAdd();

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
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

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

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
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

                    b.Property<DateTime?>("EffectiveDate");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("PersonPositionId");

                    b.Property<decimal>("Salary");

                    b.Property<double>("SalaryStep");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Year");

                    b.HasKey("Id");

                    b.HasIndex("PersonPositionId");

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

            modelBuilder.Entity("PD.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PD.Models.PersonPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractType");

                    b.Property<DateTime?>("EndDate");

                    b.Property<double>("Percentage");

                    b.Property<int?>("PersonId");

                    b.Property<int?>("PositionId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PositionId");

                    b.ToTable("PersonPositions");
                });

            modelBuilder.Entity("PD.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Number");

                    b.Property<int>("PositionContract");

                    b.Property<int>("PositionType");

                    b.Property<int>("PositionWorkload");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Positions");
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
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ChartStringId");

                    b.HasIndex("PositionId");

                    b.ToTable("PositionAccounts");
                });

            modelBuilder.Entity("PD.Models.SalaryScales.SalaryScale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ATBPercentatge");

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Guid");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("SalaryScales");
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


                    b.ToTable("Account");

                    b.HasDiscriminator().HasValue("Account");
                });

            modelBuilder.Entity("PD.Models.ChartFields.BusinessUnit", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");


                    b.ToTable("BusinessUnit");

                    b.HasDiscriminator().HasValue("BusinessUnit");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Class", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");


                    b.ToTable("Class");

                    b.HasDiscriminator().HasValue("Class");
                });

            modelBuilder.Entity("PD.Models.ChartFields.DeptID", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");

                    b.Property<int>("DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("DeptID");

                    b.HasDiscriminator().HasValue("DeptID");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Fund", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");


                    b.ToTable("Fund");

                    b.HasDiscriminator().HasValue("Fund");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Program", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");


                    b.ToTable("Program");

                    b.HasDiscriminator().HasValue("Program");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Project", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");


                    b.ToTable("Project");

                    b.HasDiscriminator().HasValue("Project");
                });

            modelBuilder.Entity("PD.Models.ChartFields.Sponsor", b =>
                {
                    b.HasBaseType("PD.Models.ChartField");


                    b.ToTable("Sponsor");

                    b.HasDiscriminator().HasValue("Sponsor");
                });

            modelBuilder.Entity("PD.Models.Compensations.FacultyCompensation", b =>
                {
                    b.HasBaseType("PD.Models.Compensations.Compensation");

                    b.Property<decimal>("ContractSuppliment");

                    b.Property<decimal>("MarketSupplement");

                    b.Property<decimal>("Merit");

                    b.Property<double>("MeritDecision");

                    b.Property<string>("MeritReason");

                    b.Property<decimal>("SpecialAdjustment");

                    b.ToTable("FacultyCompensation");

                    b.HasDiscriminator().HasValue("FacultyCompensation");
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
                    b.HasOne("PD.Models.PersonPosition", "PersonPosition")
                        .WithMany("Compensations")
                        .HasForeignKey("PersonPositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PD.Models.PersonPosition", b =>
                {
                    b.HasOne("PD.Models.Person", "Person")
                        .WithMany("PersonPositions")
                        .HasForeignKey("PersonId");

                    b.HasOne("PD.Models.Position", "Position")
                        .WithMany("PersonPositions")
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("PD.Models.PositionAccount", b =>
                {
                    b.HasOne("PD.Models.ChartString", "ChartString")
                        .WithMany("PositionAccounts")
                        .HasForeignKey("ChartStringId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PD.Models.Position", "Position")
                        .WithMany("PositionAccounts")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
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
