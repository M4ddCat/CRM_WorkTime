﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkTime.Data;

#nullable disable

namespace WorkTime.Web.Data.Migrations
{
    [DbContext(typeof(WorkTimeContext))]
    [Migration("20221128144758_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AspNetUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");

                    b.ToTable("AspNetUserRole");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "NormalizedName" }, "RoleNameIndex")
                        .IsUnique()
                        .HasFilter("([NormalizedName] IS NOT NULL)");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "RoleId" }, "IX_AspNetRoleClaims_RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "NormalizedEmail" }, "EmailIndex");

                    b.HasIndex(new[] { "NormalizedUserName" }, "UserNameIndex")
                        .IsUnique()
                        .HasFilter("([NormalizedUserName] IS NOT NULL)");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserClaims_UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUserInformation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("Photography")
                        .HasColumnType("image");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserInformation", (string)null);
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserLogins_UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUserToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WorkTime.Models.DeviceCode", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode1")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("DeviceCode");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex(new[] { "DeviceCode1" }, "IX_DeviceCodes_DeviceCode")
                        .IsUnique();

                    b.HasIndex(new[] { "Expiration" }, "IX_DeviceCodes_Expiration");

                    b.ToTable("DeviceCodes");
                });

            modelBuilder.Entity("WorkTime.Models.Invoice", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Bonus")
                        .HasColumnType("float");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date");

                    b.Property<double>("Debt")
                        .HasColumnType("float");

                    b.Property<double>("HourlyWage")
                        .HasColumnType("float");

                    b.Property<double>("HoursWorked")
                        .HasColumnType("float");

                    b.Property<double>("Issued")
                        .HasColumnType("float");

                    b.Property<int>("PaymentStateId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("RemWdebtAndBonus")
                        .HasColumnType("float")
                        .HasColumnName("RemWDebtAndBonus");

                    b.Property<double>("Remainder")
                        .HasColumnType("float");

                    b.Property<double>("SumByHours")
                        .HasColumnType("float");

                    b.Property<double>("SumWithTax")
                        .HasColumnType("float");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentStateId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("WorkTime.Models.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("bit");

                    b.Property<bool>("IsX509certificate")
                        .HasColumnType("bit")
                        .HasColumnName("IsX509Certificate");

                    b.Property<string>("Use")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Use" }, "IX_Keys_Use");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("WorkTime.Models.PaymentState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("PaymentStates");
                });

            modelBuilder.Entity("WorkTime.Models.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex(new[] { "ConsumedTime" }, "IX_PersistedGrants_ConsumedTime");

                    b.HasIndex(new[] { "Expiration" }, "IX_PersistedGrants_Expiration");

                    b.HasIndex(new[] { "SubjectId", "ClientId", "Type" }, "IX_PersistedGrants_SubjectId_ClientId_Type");

                    b.HasIndex(new[] { "SubjectId", "SessionId", "Type" }, "IX_PersistedGrants_SubjectId_SessionId_Type");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("WorkTime.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Bonus")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WorkTime.Models.TaskCommentary", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("AttachedFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("TaskId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskCommentaries");
                });

            modelBuilder.Entity("WorkTime.Models.TypeOfEmployment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Tax")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TypeOfEmployment", (string)null);
                });

            modelBuilder.Entity("WorkTime.Models.UserProject", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmpTypeId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("HourlyWage")
                        .HasColumnType("float");

                    b.Property<string>("ProjectId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EmpTypeId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProjects");
                });

            modelBuilder.Entity("WorkTime.Models.WorkTask", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("CountOfHours")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DateOfCompletion")
                        .HasColumnType("date");

                    b.Property<string>("InvoiceId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IssuerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PerformerId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProjectId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TaskStatusId")
                        .HasColumnType("int");

                    b.Property<string>("TaskText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_Tasks");

                    b.HasIndex("IssuerId");

                    b.HasIndex("PerformerId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TaskStatusId");

                    b.ToTable("WorkTasks");
                });

            modelBuilder.Entity("WorkTime.Models.WorkTaskStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_TaskStatuses");

                    b.ToTable("WorkTaskStatuses");
                });

            modelBuilder.Entity("AspNetUserRole", b =>
                {
                    b.HasOne("WorkTime.Models.AspNetRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkTime.Models.AspNetUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkTime.Models.AspNetRoleClaim", b =>
                {
                    b.HasOne("WorkTime.Models.AspNetRole", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUserClaim", b =>
                {
                    b.HasOne("WorkTime.Models.AspNetUser", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUserInformation", b =>
                {
                    b.HasOne("WorkTime.Models.AspNetUser", "User")
                        .WithMany("AspNetUserInformations")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_AspNetUserInformation_AspNetUsers");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUserLogin", b =>
                {
                    b.HasOne("WorkTime.Models.AspNetUser", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUserToken", b =>
                {
                    b.HasOne("WorkTime.Models.AspNetUser", "User")
                        .WithMany("AspNetUserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkTime.Models.Invoice", b =>
                {
                    b.HasOne("WorkTime.Models.PaymentState", "PaymentState")
                        .WithMany("Invoices")
                        .HasForeignKey("PaymentStateId")
                        .IsRequired()
                        .HasConstraintName("FK_Invoices_PaymentStates");

                    b.HasOne("WorkTime.Models.Project", "Project")
                        .WithMany("Invoices")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_Invoices_Projects");

                    b.HasOne("WorkTime.Models.AspNetUser", "User")
                        .WithMany("Invoices")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Invoices_AspNetUsers");

                    b.Navigation("PaymentState");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkTime.Models.TaskCommentary", b =>
                {
                    b.HasOne("WorkTime.Models.WorkTask", "Task")
                        .WithMany("TaskCommentaries")
                        .HasForeignKey("TaskId")
                        .IsRequired()
                        .HasConstraintName("FK_TaskCommentaries_Tasks");

                    b.HasOne("WorkTime.Models.AspNetUser", "User")
                        .WithMany("TaskCommentaries")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_TaskCommentaries_AspNetUsers");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkTime.Models.UserProject", b =>
                {
                    b.HasOne("WorkTime.Models.TypeOfEmployment", "EmpType")
                        .WithMany("UserProjects")
                        .HasForeignKey("EmpTypeId")
                        .IsRequired()
                        .HasConstraintName("FK_UserProjects_TypeOfEmployment");

                    b.HasOne("WorkTime.Models.Project", "Project")
                        .WithMany("UserProjects")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_UserProjects_Projects");

                    b.HasOne("WorkTime.Models.AspNetUser", "User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserProjects_AspNetUsers");

                    b.Navigation("EmpType");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkTime.Models.WorkTask", b =>
                {
                    b.HasOne("WorkTime.Models.AspNetUser", "Issuer")
                        .WithMany("WorkTaskIssuers")
                        .HasForeignKey("IssuerId")
                        .IsRequired()
                        .HasConstraintName("FK_Tasks_AspNetUsers1");

                    b.HasOne("WorkTime.Models.AspNetUser", "Performer")
                        .WithMany("WorkTaskPerformers")
                        .HasForeignKey("PerformerId")
                        .HasConstraintName("FK_Tasks_AspNetUsers");

                    b.HasOne("WorkTime.Models.Project", "Project")
                        .WithMany("WorkTasks")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_Tasks_Projects");

                    b.HasOne("WorkTime.Models.WorkTaskStatus", "TaskStatus")
                        .WithMany("WorkTasks")
                        .HasForeignKey("TaskStatusId")
                        .IsRequired()
                        .HasConstraintName("FK_Tasks_TaskStatuses");

                    b.Navigation("Issuer");

                    b.Navigation("Performer");

                    b.Navigation("Project");

                    b.Navigation("TaskStatus");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetRole", b =>
                {
                    b.Navigation("AspNetRoleClaims");
                });

            modelBuilder.Entity("WorkTime.Models.AspNetUser", b =>
                {
                    b.Navigation("AspNetUserClaims");

                    b.Navigation("AspNetUserInformations");

                    b.Navigation("AspNetUserLogins");

                    b.Navigation("AspNetUserTokens");

                    b.Navigation("Invoices");

                    b.Navigation("TaskCommentaries");

                    b.Navigation("UserProjects");

                    b.Navigation("WorkTaskIssuers");

                    b.Navigation("WorkTaskPerformers");
                });

            modelBuilder.Entity("WorkTime.Models.PaymentState", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("WorkTime.Models.Project", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("UserProjects");

                    b.Navigation("WorkTasks");
                });

            modelBuilder.Entity("WorkTime.Models.TypeOfEmployment", b =>
                {
                    b.Navigation("UserProjects");
                });

            modelBuilder.Entity("WorkTime.Models.WorkTask", b =>
                {
                    b.Navigation("TaskCommentaries");
                });

            modelBuilder.Entity("WorkTime.Models.WorkTaskStatus", b =>
                {
                    b.Navigation("WorkTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
