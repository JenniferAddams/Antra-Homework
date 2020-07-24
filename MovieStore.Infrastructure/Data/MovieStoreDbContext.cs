using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Entities;
namespace MovieStore.Infrastructure.Data
{
    // Install all the EF Core libraries using Nuget package Manger
    // Create a class that inherits from DbContext class
    // DbContext kinda represents your Database
    // Create a connection string which EF is gonna use to create/access the Database, should include server name, Database Name and any credentials
    // Create an Entity Class, Genre table
    // Make sure to add your Entity class as a DbSet property inside your DbContext class
    // In EF we have thing called Migrations, we are gonna use Migrations to create our Database
    // We need to add Migration commands to Create the tables, database etc
    // When running Migration commands, make sure the project selected is the one project which has DbContext class
    // Make sure you add reference for library that has DbContext to your startup project, in this case MVC
    // Tell MVC project that we are using Entity Framework in startup file
    // Add DbContext options as constructor parameter for our DbContext
    // Add-Migration MigrationName, make sure your migration names are meaningful, don't use names such as xyz, abc, migration1 like that
    // Make sure you have Migrations folder created, and check the created migration file
    // After Creating Migration file and verifying it we need to use update-database command

    // Whenever you change your model/entity always make sure you add new Migration
    // With CF approach never change the Database directly, always change the entities using DataAnnotations or Fluent API and add new migration  then update database
    // IN EF we have 2 ways to create our entities and model our database using Code-First approach
    // 1. Data Annotations which is nothing but bunch of C# attributes that we can use
    // 2. Fluent API is more syntax and more powerful and usually uses lambdas
    // Combine both DataAnnotations and Fluent API
    public class MovieStoreDbContext : DbContext
    {
        // Multiple DbSets, all the DbSets you create are gonna reside in your DbContext class
        // This DbSet, is gonna represent out Table based on Entity class which is Genre in this case
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
        {
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        //cast Table
        public DbSet<Cast> Casts { get; set; }
        //movieCast Table
        public DbSet<MovieCast> MovieCasts { get; set; }
        //User Table
        public DbSet<User> users { get; set; }
        //purchase table
        public DbSet<Purchase> Purchases { get; set; }
        //review table
        public DbSet<Review> Reviews { get; set; }
        //favorite table
        public DbSet<Favorite> Favorites { get; set; }
        //Role Table
        public DbSet<Role> Roles { get;set; }
        //userRow table
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<User>(ConfigureUsers);
            modelBuilder.Entity<Purchase>(configurePurchase);
            modelBuilder.Entity<Review>(configureReview);
            modelBuilder.Entity<Favorite>(configureFavorite);
            modelBuilder.Entity<Role>(configureRole);
            modelBuilder.Entity<UserRole>(configureUserRole);
        }

        private void configureUserRole(EntityTypeBuilder<UserRole> modelBuilder)
        {
            modelBuilder.ToTable("UserRole");
            modelBuilder.HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.HasOne(ur => ur.User).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.UserId);
            modelBuilder.HasOne(ur => ur.Role).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.RoleId);
            
        }

        private void configureRole(EntityTypeBuilder<Role> modelBuilder)
        {
            modelBuilder.ToTable("Role");
            modelBuilder.HasKey(r => r.Id);
            modelBuilder.Property(r => r.Name).HasMaxLength(20);
        }



        private void configureFavorite(EntityTypeBuilder<Favorite> modelBuilder)
        {
            modelBuilder.ToTable("Favorite");
            modelBuilder.HasKey(f => f.Id);
            modelBuilder.HasIndex(f => new { f.MovieId, f.UserId }).IsUnique();
        }

        private void configureReview(EntityTypeBuilder<Review> modelBuilder)
        {
            modelBuilder.ToTable("Review");
            modelBuilder.HasKey(r => new { r.MovieId, r.UserId });
            modelBuilder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");
            modelBuilder.Property(r => r.ReviewText).HasMaxLength(20000);

        }

        private void configurePurchase(EntityTypeBuilder<Purchase> modelBuilder)
        {
            modelBuilder.ToTable("Purchase");
            modelBuilder.HasKey(p => p.Id);
            modelBuilder.Property(p => p.UserId).IsRequired();
            modelBuilder.Property(p => p.PurchaseNumber).IsRequired();
            modelBuilder.Property(p => p.TotalPrice).IsRequired().HasColumnType("decimal(5, 2)");
            modelBuilder.Property(p => p.PurchaseDateTime).IsRequired();


        }

        private void ConfigureUsers(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.ToTable("User");
            modelBuilder.HasKey(u => u.Id);
            modelBuilder.Property(u => u.FirstName).HasMaxLength(128);
            modelBuilder.Property(u => u.LastName).HasMaxLength(128);
            modelBuilder.Property(u => u.DateOfBirth).HasColumnType("datetime2").HasMaxLength(7);
            modelBuilder.Property(u => u.Email).HasMaxLength(256);
            modelBuilder.Property(u => u.HashedPassword).HasColumnType("nvarchar(max)");
            modelBuilder.Property(u => u.Salt).HasColumnType("nvarchar(max)");
            modelBuilder.Property(u => u.PhoneNumber).HasColumnType("nvarchar(max)");
            //modelBuilder.Property(u => u.TwoFactorEnabled).IsRequired();
            modelBuilder.Property(u => u.LockoutEndDate).HasColumnType("DateTimeOffset");
            modelBuilder.Property(u => u.LastLoginDateTime).HasColumnType("datetime2");
            modelBuilder.Property(u => u.IsLocked).HasDefaultValue(false);
            //modelBuilder.Property(u => u.AccessFailedCount).IsRequired();
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> modelBuilder)
        {
            modelBuilder.ToTable("MovieCast");
            modelBuilder.HasKey(mc => new { mc.MovieId, mc.CastId,mc.Character});
            modelBuilder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.MovieId);
            modelBuilder.HasOne(mc => mc.Cast).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.CastId);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> modelBuilder)
        {
            modelBuilder.ToTable("MovieGenre");
            modelBuilder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.HasOne(mg => mg.Movie).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.MovieId);
            modelBuilder.HasOne(mg => mg.Genre).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.GenreId);
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> modelBuilder)
        {
            modelBuilder.ToTable("Trailer");
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).HasMaxLength(2084);
            modelBuilder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> modelBuilder)
        {
            // we can use Fluen API Configurations to model our tables
            modelBuilder.ToTable("Movie");
            modelBuilder.HasKey(m => m.Id);
            modelBuilder.Property(m => m.Title).IsRequired().HasMaxLength(256);
            modelBuilder.Property(m => m.Overview).HasMaxLength(4096);
            modelBuilder.Property(m => m.Tagline).HasMaxLength(512);
            modelBuilder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.PosterUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            modelBuilder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            modelBuilder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            // we don't want to Create Rating Column but we want C# rating property in our Entity so that we can show Movie rating in the UI
            modelBuilder.Ignore(m => m.Rating);
        }


    }
}