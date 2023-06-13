using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TPLab4.Data.TPModels;

namespace TPLab4.Data;

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
            entity.HasKey(e => e.Id).HasName("PK__Movie__3213E83FE1889EF6");

            entity.ToTable("Movie");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MovieBudget)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("movie_budget");
            entity.Property(e => e.MovieDuration).HasColumnName("movie_duration");
            entity.Property(e => e.MovieGenre)
                .HasMaxLength(255)
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
