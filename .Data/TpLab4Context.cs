using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TPLab4..DataTPModels;

namespace TPLab4..Data;

public partial class TpLab4Context : DbContext
{
    public TpLab4Context()
    {
    }

    public TpLab4Context(DbContextOptions<TpLab4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=tp-lab4;Trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Actor");

            entity.Property(e => e.ActorBirthdate)
                .HasColumnType("date")
                .HasColumnName("actor_birthdate");
            entity.Property(e => e.ActorName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("actor_name");
            entity.Property(e => e.ActorPicture)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("actor_picture");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Movie");

            entity.Property(e => e.MovieBudget)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("movie_budget");
            entity.Property(e => e.MovieDuration).HasColumnName("movie_duration");
            entity.Property(e => e.MovieGenre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("movie_genre");
            entity.Property(e => e.MovieName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("movie_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
