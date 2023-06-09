﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

using System;

#nullable disable

namespace BookStore.Infrastructure.Data.Migrations
{
    [DbContext(typeof(BookStoreDbContext))]
    [Migration("20230513154251_AddCategorySeed")]
    partial class AddCategorySeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookStore.Web.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("75a25bb8-a13a-488a-956c-19d3a8f5f238"),
                            DisplayOrder = 1,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = new Guid("c15d7d6c-5314-4da4-b8d7-d20345433e58"),
                            DisplayOrder = 2,
                            Name = "Sci-fi"
                        },
                        new
                        {
                            Id = new Guid("a142ae3a-3f8c-444f-a7ea-ca6b49fdea90"),
                            DisplayOrder = 3,
                            Name = "Action"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
