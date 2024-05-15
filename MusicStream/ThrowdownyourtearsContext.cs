using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicStream;

public partial class ThrowdownyourtearsContext : DbContext
{
    public ThrowdownyourtearsContext()
    {
    }

    public ThrowdownyourtearsContext(DbContextOptions<ThrowdownyourtearsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Performer> Performers { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=throwdownyourtears;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable("Album");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Creatorid).HasColumnName("creatorid");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("duration");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Imagesource)
               .HasMaxLength(600)
               .IsUnicode(false)
               .HasColumnName("imagesource");

            entity.HasOne(d => d.Creator).WithMany(p => p.Albums)
                .HasForeignKey(d => d.Creatorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Album_Performer");
        });

        modelBuilder.Entity<Performer>(entity =>
        {
            entity.ToTable("Performer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Auditions)
                .HasDefaultValueSql("((0))")
                .HasColumnName("auditions");
            entity.Property(e => e.Nick)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nick");
            entity.Property(e => e.Imagesource)
                .HasMaxLength(600)
                .IsUnicode(false)
                .HasColumnName("imagesource");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Regdate)
                .HasColumnType("datetime")
                .HasColumnName("regdate");
        });

        

        modelBuilder.Entity<Track>(entity =>
        {
            entity.ToTable("Track");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Albumid).HasColumnName("albumid");
            entity.Property(e => e.Auditions)
                .HasDefaultValueSql("((0))")
                .HasColumnName("auditions");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("duration");
            entity.Property(e => e.Filename)
                .HasMaxLength(600)
                .IsUnicode(false)
                .HasColumnName("filename");
            entity.Property(e => e.Imagesource)
                .HasMaxLength(600)
                .IsUnicode(false)
                .HasColumnName("imagesource");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.Album).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.Albumid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Track_Album");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nick)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nick");
            entity.Property(e => e.Imagesource)
                .HasMaxLength(600)
                .IsUnicode(false)
                .HasColumnName("imagesource");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Regdate)
                .HasColumnType("datetime")
                .HasColumnName("regdate");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
