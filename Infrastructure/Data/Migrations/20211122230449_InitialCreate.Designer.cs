﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211122230449_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Russian_Russia.1251")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.Property<int>("BooksId")
                        .HasColumnType("integer");

                    b.Property<int>("GenresId")
                        .HasColumnType("integer");

                    b.HasKey("BooksId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("BookGenre");
                });

            modelBuilder.Entity("BookPerson", b =>
                {
                    b.Property<int>("BooksId")
                        .HasColumnType("integer");

                    b.Property<int>("PeopleId")
                        .HasColumnType("integer");

                    b.HasKey("BooksId", "PeopleId");

                    b.HasIndex("PeopleId");

                    b.ToTable("LibraryCard");
                });

            modelBuilder.Entity("Core.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("middle_name");

                    b.HasKey("Id");

                    b.ToTable("author");
                });

            modelBuilder.Entity("Core.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer")
                        .HasColumnName("author_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("book");
                });

            modelBuilder.Entity("Core.Entities.BookGenre", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("book_id");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer")
                        .HasColumnName("genre_id");

                    b.HasKey("BookId", "GenreId")
                        .HasName("book_genre_pkey");

                    b.HasIndex("GenreId");

                    b.ToTable("book_genre");
                });

            modelBuilder.Entity("Core.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("genre_name");

                    b.HasKey("Id");

                    b.ToTable("genre");
                });

            modelBuilder.Entity("Core.Entities.LibraryCard", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("book_id");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer")
                        .HasColumnName("person_id");

                    b.HasKey("BookId", "PersonId")
                        .HasName("library_card_pkey");

                    b.HasIndex("PersonId");

                    b.ToTable("library_card");
                });

            modelBuilder.Entity("Core.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("middle_name");

                    b.HasKey("Id");

                    b.ToTable("person");
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.HasOne("Core.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookPerson", b =>
                {
                    b.HasOne("Core.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("PeopleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.Book", b =>
                {
                    b.HasOne("Core.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("book_author_id_fkey");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Core.Entities.BookGenre", b =>
                {
                    b.HasOne("Core.Entities.Book", "Book")
                        .WithMany("BookGenres")
                        .HasForeignKey("BookId")
                        .HasConstraintName("book_genre_book_id_fkey")
                        .IsRequired();

                    b.HasOne("Core.Entities.Genre", "Genre")
                        .WithMany("BookGenres")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("book_genre_genre_id_fkey")
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Core.Entities.LibraryCard", b =>
                {
                    b.HasOne("Core.Entities.Book", "Book")
                        .WithMany("LibraryCards")
                        .HasForeignKey("BookId")
                        .HasConstraintName("library_card_book_id_fkey")
                        .IsRequired();

                    b.HasOne("Core.Entities.Person", "Person")
                        .WithMany("LibraryCards")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("library_card_person_id_fkey")
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Core.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Core.Entities.Book", b =>
                {
                    b.Navigation("BookGenres");

                    b.Navigation("LibraryCards");
                });

            modelBuilder.Entity("Core.Entities.Genre", b =>
                {
                    b.Navigation("BookGenres");
                });

            modelBuilder.Entity("Core.Entities.Person", b =>
                {
                    b.Navigation("LibraryCards");
                });
#pragma warning restore 612, 618
        }
    }
}
