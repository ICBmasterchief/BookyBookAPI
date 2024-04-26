﻿// <auto-generated />
using System;
using BookyBook.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookyBook.Data.Migrations
{
    [DbContext(typeof(BookyBookContext))]
    [Migration("20240407120414_firstMigration")]
    partial class firstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookyBook.Models.Book", b =>
                {
                    b.Property<int>("IdNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNumber"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Copies")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Score")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("IdNumber");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            IdNumber = 10001,
                            Author = "j. r. r. tolkien",
                            Copies = 1,
                            Genre = "fantasy",
                            Score = 8.8m,
                            Title = "the fellowship of the ring",
                            Year = 1954
                        },
                        new
                        {
                            IdNumber = 10002,
                            Author = "arturo perez reverte",
                            Copies = 1,
                            Genre = "historical novel",
                            Score = 7.44m,
                            Title = "el capitan alatriste",
                            Year = 1996
                        },
                        new
                        {
                            IdNumber = 10003,
                            Author = "mary shelley",
                            Copies = 2,
                            Genre = "gotic novel",
                            Score = 7.8m,
                            Title = "frankenstein",
                            Year = 1818
                        },
                        new
                        {
                            IdNumber = 10004,
                            Author = "j. k. rowling",
                            Copies = 1,
                            Genre = "fantasy",
                            Score = 8m,
                            Title = "harry potter y la piedra filosofal",
                            Year = 1997
                        });
                });

            modelBuilder.Entity("BookyBook.Models.Borrowing", b =>
                {
                    b.Property<int>("IdNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNumber"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BorrowingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateToReturn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PenaltyFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Returned")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ReturnedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("IdNumber");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Borrowings");

                    b.HasData(
                        new
                        {
                            IdNumber = 1,
                            BookId = 10001,
                            BorrowingDate = new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateToReturn = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PenaltyFee = 0m,
                            Returned = true,
                            ReturnedDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1111
                        },
                        new
                        {
                            IdNumber = 2,
                            BookId = 10003,
                            BorrowingDate = new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateToReturn = new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PenaltyFee = 10m,
                            Returned = true,
                            ReturnedDate = new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1112
                        },
                        new
                        {
                            IdNumber = 3,
                            BookId = 10004,
                            BorrowingDate = new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateToReturn = new DateTime(2024, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PenaltyFee = 0m,
                            Returned = false,
                            UserId = 1111
                        });
                });

            modelBuilder.Entity("BookyBook.Models.User", b =>
                {
                    b.Property<int>("IdNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNumber"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PenaltyFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdNumber");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            IdNumber = 1111,
                            Email = "ignaciocasaus1cns@gmail.com",
                            Name = "Ignacio",
                            Password = "patata",
                            PenaltyFee = 0m,
                            RegistrationDate = new DateTime(2024, 4, 4, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            IdNumber = 1112,
                            Email = "emaildealex@gmail.com",
                            Name = "Alex",
                            Password = "pimiento",
                            PenaltyFee = 0m,
                            RegistrationDate = new DateTime(2024, 4, 5, 18, 30, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("BookyBook.Models.Borrowing", b =>
                {
                    b.HasOne("BookyBook.Models.Book", null)
                        .WithMany("Borrowings")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookyBook.Models.User", null)
                        .WithMany("Borrowings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookyBook.Models.Book", b =>
                {
                    b.Navigation("Borrowings");
                });

            modelBuilder.Entity("BookyBook.Models.User", b =>
                {
                    b.Navigation("Borrowings");
                });
#pragma warning restore 612, 618
        }
    }
}