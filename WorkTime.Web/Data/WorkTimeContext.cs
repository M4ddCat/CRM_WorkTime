﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using WorkTime.Models;

namespace WorkTime.Data;

public partial class WorkTimeContext : DbContext
{
    public WorkTimeContext()
    {
    }

    public WorkTimeContext(DbContextOptions<WorkTimeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserInformation> AspNetUserInformations { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<DeviceCode> DeviceCodes { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceFile> InvoiceFiles { get; set; }

    public virtual DbSet<Key> Keys { get; set; }

    public virtual DbSet<PaymentState> PaymentStates { get; set; }

    public virtual DbSet<PersistedGrant> PersistedGrants { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<TaskCommentary> TaskCommentaries { get; set; }

    public virtual DbSet<TypeOfEmployment> TypeOfEmployments { get; set; }

    public virtual DbSet<UserProject> UserProjects { get; set; }

    public virtual DbSet<WorkTask> WorkTasks { get; set; }

    public virtual DbSet<WorkTaskStatus> WorkTaskStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("workstation id=asdasd123aa.mssql.somee.com;packet size=4096;user id=AndreyIgonin_SQLLogin_1;pwd=u5ijv7iv5o;data source=asdasd123aa.mssql.somee.com;persist security info=False;initial catalog=asdasd123aa");
    //Server=DESKTOP-OCA11UA;Database=WT_TwinTech3;Trusted_Connection=True;TrustServerCertificate=True

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);

            entity.HasData(
                new AspNetRole { Id = $"{Guid.NewGuid()}", Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new AspNetRole { Id = $"{Guid.NewGuid()}", Name = "Manager", NormalizedName = "MANAGER" },
                new AspNetRole { Id = $"{Guid.NewGuid()}", Name = "Bookkeeper", NormalizedName = "BOOKKEEPER" }
                );
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserInformation>(entity =>
        {
            entity.ToTable("AspNetUserInformation");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Patronymic).HasMaxLength(100);
            entity.Property(e => e.Photography).HasColumnType("image");
            entity.Property(e => e.Surname).HasMaxLength(100);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserInformations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUserInformation_AspNetUsers");
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<DeviceCode>(entity =>
        {
            entity.HasKey(e => e.UserCode);

            entity.HasIndex(e => e.DeviceCode1, "IX_DeviceCodes_DeviceCode").IsUnique();

            entity.HasIndex(e => e.Expiration, "IX_DeviceCodes_Expiration");

            entity.Property(e => e.UserCode).HasMaxLength(200);
            entity.Property(e => e.ClientId).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.DeviceCode1)
                .HasMaxLength(200)
                .HasColumnName("DeviceCode");
            entity.Property(e => e.SessionId).HasMaxLength(100);
            entity.Property(e => e.SubjectId).HasMaxLength(200);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.ProjectId).HasMaxLength(450);
            entity.Property(e => e.RemWdebtAndBonus).HasColumnName("RemWDebtAndBonus");
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.PaymentState).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PaymentStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_PaymentStates");

            entity.HasOne(d => d.Project).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_Invoices_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_AspNetUsers");
        });

        modelBuilder.Entity<InvoiceFile>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.InvoiceId).HasMaxLength(450);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceFiles)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceFiles_Invoices");

        });

        modelBuilder.Entity<Key>(entity =>
        {
            entity.HasIndex(e => e.Use, "IX_Keys_Use");

            entity.Property(e => e.Algorithm).HasMaxLength(100);
            entity.Property(e => e.IsX509certificate).HasColumnName("IsX509Certificate");
        });

        modelBuilder.Entity<PaymentState>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.HasData(
                new PaymentState { Id = 1, Name = "Сформирован" }, 
                new PaymentState { Id = 2, Name = "Оплачен" },
                new PaymentState { Id = 3, Name = "Подтверждён" });
        });

        modelBuilder.Entity<PersistedGrant>(entity =>
        {
            entity.HasKey(e => e.Key);

            entity.HasIndex(e => e.ConsumedTime, "IX_PersistedGrants_ConsumedTime");

            entity.HasIndex(e => e.Expiration, "IX_PersistedGrants_Expiration");

            entity.HasIndex(e => new { e.SubjectId, e.ClientId, e.Type }, "IX_PersistedGrants_SubjectId_ClientId_Type");

            entity.HasIndex(e => new { e.SubjectId, e.SessionId, e.Type }, "IX_PersistedGrants_SubjectId_SessionId_Type");

            entity.Property(e => e.Key).HasMaxLength(200);
            entity.Property(e => e.ClientId).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.SessionId).HasMaxLength(100);
            entity.Property(e => e.SubjectId).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TaskCommentary>(entity =>
        {
            entity.Property(e => e.TaskId).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Task).WithMany(p => p.TaskCommentaries)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskCommentaries_Tasks");

            entity.HasOne(d => d.User).WithMany(p => p.TaskCommentaries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskCommentaries_AspNetUsers");
        });

        modelBuilder.Entity<TypeOfEmployment>(entity =>
        {
            entity.ToTable("TypeOfEmployment");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasData(
                new TypeOfEmployment { Id = "1", Name = "Устроен по ТК", Tax = 13 },
                new TypeOfEmployment { Id = "2", Name = "Самозанятый", Tax = 13 }
            );
        });

        modelBuilder.Entity<UserProject>(entity =>
        {
            entity.Property(e => e.EmpTypeId).HasMaxLength(450);
            entity.Property(e => e.ProjectId).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.EmpType).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.EmpTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProjects_TypeOfEmployment");

            entity.HasOne(d => d.Project).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_UserProjects_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProjects_AspNetUsers");
        });

        modelBuilder.Entity<WorkTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tasks");

            entity.Property(e => e.DateOfCompletion).HasColumnType("date");
            entity.Property(e => e.InvoiceId).HasMaxLength(450);
            entity.Property(e => e.IssuerId).HasMaxLength(450);
            entity.Property(e => e.PerformerId).HasMaxLength(450);
            entity.Property(e => e.ProjectId).HasMaxLength(450);
            entity.Property(e => e.TaskName).HasMaxLength(100);

            entity.HasOne(d => d.Issuer).WithMany(p => p.WorkTaskIssuers)
                .HasForeignKey(d => d.IssuerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_AspNetUsers1");

            entity.HasOne(d => d.Performer).WithMany(p => p.WorkTaskPerformers)
                .HasForeignKey(d => d.PerformerId)
                .HasConstraintName("FK_Tasks_AspNetUsers");

            entity.HasOne(d => d.Project).WithMany(p => p.WorkTasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_Tasks_Projects");

            entity.HasOne(d => d.TaskStatus).WithMany(p => p.WorkTasks)
                .HasForeignKey(d => d.TaskStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_TaskStatuses");
        });

        modelBuilder.Entity<WorkTaskStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TaskStatuses");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasData(
                new WorkTaskStatus { Id = 1, Name = "Сформирована" },
                new WorkTaskStatus { Id = 2, Name = "Выполняется" },
                new WorkTaskStatus { Id = 3, Name = "Ожидает проверки" },
                new WorkTaskStatus { Id = 4, Name = "Завершена" });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
