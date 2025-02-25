﻿// <auto-generated />
using System;
using DayStory.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DayStory.Infrastructure.Migrations
{
    [DbContext(typeof(DayStoryAPIDbContext))]
    [Migration("20240530111402_initMigrations")]
    partial class initMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DayStory.Domain.Entities.ArtStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("ArtStyle", "DayStoryDb");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Realist"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Surrealist"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Minimalist"
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Retro/Vintage"
                        },
                        new
                        {
                            Id = 5,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Steampunk"
                        },
                        new
                        {
                            Id = 6,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Cyberpunk"
                        },
                        new
                        {
                            Id = 7,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Futuristic"
                        },
                        new
                        {
                            Id = 8,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Baroque"
                        },
                        new
                        {
                            Id = 9,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Gothic"
                        },
                        new
                        {
                            Id = 10,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Pop Art"
                        },
                        new
                        {
                            Id = 11,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Abstract"
                        },
                        new
                        {
                            Id = 12,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Art Deco"
                        },
                        new
                        {
                            Id = 13,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 14,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Digital Art"
                        },
                        new
                        {
                            Id = 15,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Cinematic"
                        },
                        new
                        {
                            Id = 16,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Anime"
                        },
                        new
                        {
                            Id = 17,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Cartoonist"
                        },
                        new
                        {
                            Id = 18,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Comic"
                        },
                        new
                        {
                            Id = 19,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Retro Pop"
                        },
                        new
                        {
                            Id = 20,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Logo"
                        },
                        new
                        {
                            Id = 21,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Whimsical"
                        },
                        new
                        {
                            Id = 22,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 23,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Monster"
                        },
                        new
                        {
                            Id = 24,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Figure"
                        },
                        new
                        {
                            Id = 25,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Retro Sci-Fi"
                        },
                        new
                        {
                            Id = 26,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Dark Fantasy"
                        },
                        new
                        {
                            Id = 27,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Dreamwave"
                        },
                        new
                        {
                            Id = 28,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Mystical"
                        },
                        new
                        {
                            Id = 29,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Expressionism"
                        },
                        new
                        {
                            Id = 30,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Flora"
                        },
                        new
                        {
                            Id = 31,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Daydream"
                        },
                        new
                        {
                            Id = 32,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Radioactive"
                        },
                        new
                        {
                            Id = 33,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Starry"
                        });
                });

            modelBuilder.Entity("DayStory.Domain.Entities.DaySummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArtStyleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Date")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValueSql("TO_CHAR(NOW(), 'DD-MM-YYYY')");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ArtStyleId");

                    b.HasIndex("UserId");

                    b.ToTable("DaySummary", "DayStoryDb");
                });

            modelBuilder.Entity("DayStory.Domain.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Date")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValueSql("TO_CHAR(NOW(), 'DD-MM-YYYY')");

                    b.Property<int?>("DaySummaryId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasMaxLength(350)
                        .HasColumnType("character varying(350)");

                    b.Property<int>("Priority")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("Time")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DaySummaryId");

                    b.HasIndex("UserId");

                    b.ToTable("Event", "DayStoryDb");
                });

            modelBuilder.Entity("DayStory.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Gender")
                        .HasMaxLength(50)
                        .HasColumnType("integer");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ProfilePicturePath")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("User");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User", "DayStoryDb");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = "00-00-0000",
                            CreatedOn = new DateTime(2024, 5, 30, 11, 14, 2, 400, DateTimeKind.Utc).AddTicks(8750),
                            Email = "admin",
                            FirstName = "admin",
                            Gender = 0,
                            HashedPassword = "AQAAAAIAAYagAAAAEMAjbf9FW8x1DZZ+AyQXeu64godJj2gAwx6EPpTyTL1El6DlT1fXmkdw/+a81zac3g==",
                            IsDeleted = false,
                            LastName = "admin",
                            Role = "Admin",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = "24-03-1999",
                            CreatedOn = new DateTime(2024, 5, 30, 11, 14, 2, 400, DateTimeKind.Utc).AddTicks(8759),
                            Email = "nisa@gmail.com",
                            FirstName = "Nisa",
                            Gender = 2,
                            HashedPassword = "AQAAAAIAAYagAAAAEEbO/nyiGYmwOTIe8lSZOx5V0fA3uJCJoajax2zFl9ZSSMbM10M27yrBbwzUPCc6gg==",
                            IsDeleted = false,
                            LastName = "Turhan",
                            Role = "User",
                            Username = "nisatur"
                        });
                });

            modelBuilder.Entity("DayStory.Domain.Entities.DaySummary", b =>
                {
                    b.HasOne("DayStory.Domain.Entities.ArtStyle", "ArtStyle")
                        .WithMany("DaySummaries")
                        .HasForeignKey("ArtStyleId");

                    b.HasOne("DayStory.Domain.Entities.User", "User")
                        .WithMany("DaySummaries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtStyle");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DayStory.Domain.Entities.Event", b =>
                {
                    b.HasOne("DayStory.Domain.Entities.DaySummary", "DaySummary")
                        .WithMany("Events")
                        .HasForeignKey("DaySummaryId");

                    b.HasOne("DayStory.Domain.Entities.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DaySummary");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DayStory.Domain.Entities.ArtStyle", b =>
                {
                    b.Navigation("DaySummaries");
                });

            modelBuilder.Entity("DayStory.Domain.Entities.DaySummary", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("DayStory.Domain.Entities.User", b =>
                {
                    b.Navigation("DaySummaries");

                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
