﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wsr.Data;

namespace Wsr.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20220426202037_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wsr.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80563b60-d587-4fac-86ee-df1c0c3dfc2c"),
                            HashedPassword = "p2i35nhjp1ip",
                            IsAdmin = true,
                            Name = "Zbyszek"
                        },
                        new
                        {
                            Id = new Guid("510de3e6-76d8-4809-b165-a34043d4cddc"),
                            HashedPassword = "p2i35nhjp1ip",
                            IsAdmin = false,
                            Name = "Marcel"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}