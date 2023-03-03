using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PiWorksAnswer2.Data;

public partial class exmp : DbContext
{
    public exmp()
    {
    }

    public exmp(DbContextOptions<exmp> options)
        : base(options)
    {
    }

    public virtual DbSet<NewTable> NewTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=exmp;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<NewTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("new_table");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeviceType)
                .HasMaxLength(255)
                .HasColumnName("Device_Type");
            entity.Property(e => e.StatsAccessLink)
                .HasMaxLength(255)
                .HasColumnName("Stats_Access_Link");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
