﻿// <auto-generated />
using System;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlightDocsSystem.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlightDocsSystem.Model.CargoManifest", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlightNo")
                        .HasColumnType("int");

                    b.Property<string>("PointOfLoading")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PointOfUnLoading")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("FlightId");

                    b.ToTable("CargoManifests");
                });

            modelBuilder.Entity("FlightDocsSystem.Model.DocumentList", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DocumentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DocumentId");

                    b.HasIndex("UserId");

                    b.ToTable("DocumentLists");
                });

            modelBuilder.Entity("FlightDocsSystem.Model.GroupPermission", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GroupId");

                    b.ToTable("GroupPermissions");
                });

            modelBuilder.Entity("FlightDocsSystem.Model.RoleModel", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("RoleModels");
                });

            modelBuilder.Entity("FlightDocsSystem.Model.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupPermissionGroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("UserBlock")
                        .HasColumnType("bit");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserGender")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserPassword")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("UserRoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UserStatus")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("GroupPermissionGroupId");

                    b.HasIndex("UserRole");

                    b.ToTable("UserModels");
                });

            modelBuilder.Entity("FlightDocsSystem.Model.DocumentList", b =>
                {
                    b.HasOne("FlightDocsSystem.Model.UserModel", "User")
                        .WithMany("documentLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightDocsSystem.Model.UserModel", b =>
                {
                    b.HasOne("FlightDocsSystem.Model.GroupPermission", "GroupPermission")
                        .WithMany("users")
                        .HasForeignKey("GroupPermissionGroupId");

                    b.HasOne("FlightDocsSystem.Model.RoleModel", "roleModel")
                        .WithMany()
                        .HasForeignKey("UserRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupPermission");

                    b.Navigation("roleModel");
                });

            modelBuilder.Entity("FlightDocsSystem.Model.GroupPermission", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("FlightDocsSystem.Model.UserModel", b =>
                {
                    b.Navigation("documentLists");
                });
#pragma warning restore 612, 618
        }
    }
}