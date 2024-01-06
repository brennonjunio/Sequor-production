﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sequorProduction.DataContext;

#nullable disable

namespace Sequor_production.Migrations
{
    [DbContext(typeof(DataBase))]
    partial class DataBaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Material", b =>
                {
                    b.Property<string>("materialCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("materialDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("materialCode");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<string>("order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("productCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quantity")
                         .HasColumnType("real")
                        .HasDefaultValue(18.2);

                    b.HasKey("order");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.Property<string>("productCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("cycleTime")
                        .HasColumnType("real")
                        .HasDefaultValue(18.2);

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("productCode");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Production", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<decimal>("cycleTime")
                        .HasColumnType("real")
                        .HasDefaultValue(18.2);

                    b.Property<DateTime>("date")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("materialCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("order")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quantity")
                        .ValueGeneratedOnAdd()
                      .HasColumnType("real")
                        .HasDefaultValue(18.2);

                    b.HasKey("id");

                    b.ToTable("Production");
                });

            modelBuilder.Entity("ProductMaterial", b =>
                {
                    b.Property<string>("productCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("materialCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("productCode", "materialCode");

                    b.ToTable("ProductMaterial");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("initialDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("email");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
