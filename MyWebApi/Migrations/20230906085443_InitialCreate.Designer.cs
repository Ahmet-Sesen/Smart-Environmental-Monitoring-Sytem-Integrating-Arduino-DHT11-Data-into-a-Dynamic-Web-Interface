﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWebApi.Models;

#nullable disable

namespace MyWebApi.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230906085443_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyWebApi.Models.NemSicaklikVerisi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Nem")
                        .HasColumnType("float");

                    b.Property<DateTime>("Saat")
                        .HasColumnType("datetime2");

                    b.Property<double>("Sicaklik")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("NemSicaklikVerileri");
                });
#pragma warning restore 612, 618
        }
    }
}
