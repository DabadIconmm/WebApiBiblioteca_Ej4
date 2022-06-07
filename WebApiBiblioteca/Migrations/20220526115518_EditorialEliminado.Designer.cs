﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiBiblioteca;

#nullable disable

namespace WebApiBiblioteca.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220526115518_EditorialEliminado")]
    partial class EditorialEliminado
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApiBiblioteca.Entidades.Editorial", b =>
                {
                    b.Property<int>("EditorialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EditorialId"), 1L, 1);

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EditorialId");

                    b.ToTable("Editoriales");

                    b.HasData(
                        new
                        {
                            EditorialId = 1,
                            Eliminado = false,
                            Nombre = "Editorial 1"
                        },
                        new
                        {
                            EditorialId = 2,
                            Eliminado = false,
                            Nombre = "Editorial 2"
                        });
                });

            modelBuilder.Entity("WebApiBiblioteca.Entidades.Libro", b =>
                {
                    b.Property<int>("LibroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibroId"), 1L, 1);

                    b.Property<int>("EditorialId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Paginas")
                        .HasColumnType("int");

                    b.HasKey("LibroId");

                    b.HasIndex("EditorialId");

                    b.ToTable("Libros");

                    b.HasData(
                        new
                        {
                            LibroId = 1,
                            EditorialId = 1,
                            Nombre = "Libro 1",
                            Paginas = 50
                        },
                        new
                        {
                            LibroId = 2,
                            EditorialId = 2,
                            Nombre = "Libro 2",
                            Paginas = 500
                        },
                        new
                        {
                            LibroId = 3,
                            EditorialId = 1,
                            Nombre = "Libro 3",
                            Paginas = 1500
                        },
                        new
                        {
                            LibroId = 4,
                            EditorialId = 2,
                            Nombre = "Libro 4",
                            Paginas = 850
                        },
                        new
                        {
                            LibroId = 5,
                            EditorialId = 1,
                            Nombre = "Libro 5",
                            Paginas = 700
                        },
                        new
                        {
                            LibroId = 6,
                            EditorialId = 2,
                            Nombre = "Libro 6",
                            Paginas = 4000
                        },
                        new
                        {
                            LibroId = 7,
                            EditorialId = 1,
                            Nombre = "Libro 7",
                            Paginas = 550
                        },
                        new
                        {
                            LibroId = 8,
                            EditorialId = 2,
                            Nombre = "Libro 8",
                            Paginas = 150
                        },
                        new
                        {
                            LibroId = 9,
                            EditorialId = 1,
                            Nombre = "Libro 9",
                            Paginas = 100
                        },
                        new
                        {
                            LibroId = 10,
                            EditorialId = 2,
                            Nombre = "Libro 10",
                            Paginas = 50
                        });
                });

            modelBuilder.Entity("WebApiBiblioteca.Entidades.Libro", b =>
                {
                    b.HasOne("WebApiBiblioteca.Entidades.Editorial", "Editorial")
                        .WithMany("Libros")
                        .HasForeignKey("EditorialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Editorial");
                });

            modelBuilder.Entity("WebApiBiblioteca.Entidades.Editorial", b =>
                {
                    b.Navigation("Libros");
                });
#pragma warning restore 612, 618
        }
    }
}
