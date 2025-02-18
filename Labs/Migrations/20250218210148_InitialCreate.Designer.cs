﻿// <auto-generated />
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lab1.Migrations
{
    [DbContext(typeof(UniversitydbContext))]
    [Migration("20250218210148_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lab1.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseDescription")
                        .HasColumnType("text");

                    b.Property<string>("CourseName")
                        .HasColumnType("text");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Lab1.Models.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FacultyId"));

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FacultyId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Lab1.Models.Student", b =>
                {
                    b.Property<long>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("StudentId"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FacultyId")
                        .HasColumnType("integer");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("StudentId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Lab1.Models.StudentsCourses", b =>
                {
                    b.Property<int>("StudentsCoursesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StudentsCoursesId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<long?>("StudentId1")
                        .HasColumnType("bigint");

                    b.HasKey("StudentsCoursesId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId1");

                    b.ToTable("StudentsCourses");
                });

            modelBuilder.Entity("Lab1.Models.Student", b =>
                {
                    b.HasOne("Lab1.Models.Faculty", "Faculty")
                        .WithMany("Students")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Lab1.Models.StudentsCourses", b =>
                {
                    b.HasOne("Lab1.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab1.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId1");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Lab1.Models.Faculty", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
