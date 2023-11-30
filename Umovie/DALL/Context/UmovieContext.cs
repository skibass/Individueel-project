using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DALL;

public partial class UmovieContext : DbContext
{
    public UmovieContext()
    {
    }

    public UmovieContext(DbContextOptions<UmovieContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieCategory> MovieCategories { get; set; }

    public virtual DbSet<MovieRating> MovieRatings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFavoriteMovie> UserFavoriteMovies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=127.0.0.1;Database=umovie;Uid=root;Pwd=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PRIMARY");

            entity.ToTable("movies");

            entity.Property(e => e.MovieId)
                .HasColumnType("int(11)")
                .HasColumnName("movie_id");
            entity.Property(e => e.MovieAgeRating)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("movie_age_rating");
            entity.Property(e => e.MovieDescription)
                .HasMaxLength(45)
                .HasDefaultValueSql("'''\"\"'''")
                .HasColumnName("movie_description");
            entity.Property(e => e.MovieDirector)
                .HasMaxLength(45)
                .HasDefaultValueSql("'''\"\"'''")
                .HasColumnName("movie_director");
            entity.Property(e => e.MovieLanguage)
                .HasMaxLength(45)
                .HasDefaultValueSql("'''\"\"'''")
                .HasColumnName("movie_language");
            entity.Property(e => e.MovieName)
                .HasMaxLength(45)
                .HasDefaultValueSql("'''\"\"'''")
                .HasColumnName("movie_name");
            entity.Property(e => e.MovieReleaseDate)
                .HasMaxLength(45)
                .HasDefaultValueSql("'''\"\"'''")
                .HasColumnName("movie_release_date");
        });

        modelBuilder.Entity<MovieCategory>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.MovieId, e.CategorieId }).HasName("PRIMARY");

            entity.ToTable("movie_categories");

            entity.HasIndex(e => e.CategorieId, "categorie_id_key_idx");

            entity.HasIndex(e => e.MovieId, "movie_id_key_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.MovieId)
                .HasColumnType("int(11)")
                .HasColumnName("movie_id");
            entity.Property(e => e.CategorieId)
                .HasColumnType("int(11)")
                .HasColumnName("categorie_id");

            entity.HasOne(d => d.Categorie).WithMany(p => p.MovieCategories)
                .HasForeignKey(d => d.CategorieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categorie_id_key");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieCategories)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("movie_id_key");
        });

        modelBuilder.Entity<MovieRating>(entity =>
        {
            entity.HasKey(e => new { e.RatingId, e.UserId, e.MovieId }).HasName("PRIMARY");

            entity.ToTable("movie_ratings");

            entity.HasIndex(e => e.MovieId, "rated_movie_key_idx");

            entity.HasIndex(e => e.UserId, "user_rating_key_idx");

            entity.Property(e => e.RatingId)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("rating_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.MovieId)
                .HasColumnType("int(11)")
                .HasColumnName("movie_id");
            entity.Property(e => e.RatingNumber)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("rating_number");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieRatings)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rated_movie_key");

            entity.HasOne(d => d.User).WithMany(p => p.MovieRatings)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_rating_key");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserId, "user_id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.RoleId, "user_role_key_idx");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("password");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_key");
        });

        modelBuilder.Entity<UserFavoriteMovie>(entity =>
        {
            entity.HasKey(e => new { e.FavoriteId, e.UserId, e.MovieId }).HasName("PRIMARY");

            entity.ToTable("user_favorite_movies");

            entity.HasIndex(e => e.MovieId, "favorite_movie_key_idx");

            entity.HasIndex(e => e.UserId, "user_favorite_key_idx");

            entity.Property(e => e.FavoriteId)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("favorite_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.MovieId)
                .HasColumnType("int(11)")
                .HasColumnName("movie_id");

            entity.HasOne(d => d.Movie).WithMany(p => p.UserFavoriteMovies)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("favorite_movie_key");

            entity.HasOne(d => d.User).WithMany(p => p.UserFavoriteMovies)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_favorite_key");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
