﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebApi2.Migrations
{
    [DbContext(typeof(DomainContext))]
    partial class DomainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Assignment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Grade")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ModuleID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ModuleID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Module", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Assignment", b =>
                {
                    b.HasOne("Module", "Module")
                        .WithMany("Assignments")
                        .HasForeignKey("ModuleID");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Module", b =>
                {
                    b.HasOne("Course", "Course")
                        .WithMany("Modules")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Course", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("Module", b =>
                {
                    b.Navigation("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}
