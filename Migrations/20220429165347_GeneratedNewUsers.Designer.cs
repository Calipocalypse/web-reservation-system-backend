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
    [Migration("20220429165347_GeneratedNewUsers")]
    partial class GeneratedNewUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wsr.Models.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cookie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

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

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("91c1656a-a8c8-44ee-a240-c932ef949716"),
                            HashedPassword = "065169b685c379acbec4694db0ac9e4a56adb633b83704fd3870457295e160e2",
                            IsAdmin = true,
                            Name = "Zbyszek",
                            Salt = "82lgUI2ME1Ebxk1u"
                        },
                        new
                        {
                            Id = new Guid("88cdc4f6-28e6-4631-ba27-4f7b10ee0e2d"),
                            HashedPassword = "4a3b2ecba8a891d8ea46bc1aded65e12d0bf3bec0f416f7db227e1e439d4fc2d",
                            IsAdmin = false,
                            Name = "Marcel",
                            Salt = "gS+8rsMoszFIjAeC"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
