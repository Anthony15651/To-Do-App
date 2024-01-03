﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using To_Do_App.Data;

#nullable disable

namespace To_Do_App.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("To_Do_App.Models.TaskModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("category")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("dueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isComplete")
                        .HasColumnType("bit");

                    b.Property<int>("priorityLevel")
                        .HasColumnType("int");

                    b.Property<string>("taskName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            category = 0,
                            description = "Create basic CRUD functionality so ToDo list is usable",
                            dueDate = new DateTime(1, 1, 1, 15, 30, 0, 0, DateTimeKind.Unspecified),
                            isComplete = false,
                            priorityLevel = 2,
                            taskName = "Finish Basic Functionality"
                        },
                        new
                        {
                            Id = 2,
                            category = 0,
                            description = "Add additional user-friendly features to enhance ToDo App",
                            dueDate = new DateTime(1, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            isComplete = false,
                            priorityLevel = 2,
                            taskName = "Add Additional Features"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
