﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Moto.Persistence.Contexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Moto.Persistence.Migrations
{
    [DbContext(typeof(MotoDbContext))]
    [Migration("20240924210843_ChangeDateTimeToDateOnly")]
    partial class ChangeDateTimeToDateOnly
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Moto.Domain.Entities.Courier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("DrivingLicenseImagePath")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("DrivingLicenseType")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.HasKey("Id");

                    b.ToTable("Courier");
                });

            modelBuilder.Entity("Moto.Domain.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("OccuredOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Moto.Domain.Entities.Motorcycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<short>("Year")
                        .HasMaxLength(50)
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Motorcycle");
                });

            modelBuilder.Entity("Moto.Domain.Entities.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CostPerDay")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.Property<decimal>("Fee")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.HasKey("Id");

                    b.ToTable("Plan");

                    b.HasData(
                        new
                        {
                            Id = 7,
                            CostPerDay = 30m,
                            Fee = 0.2m
                        },
                        new
                        {
                            Id = 15,
                            CostPerDay = 28m,
                            Fee = 0.4m
                        },
                        new
                        {
                            Id = 30,
                            CostPerDay = 22m,
                            Fee = 0m
                        },
                        new
                        {
                            Id = 45,
                            CostPerDay = 20m,
                            Fee = 0m
                        },
                        new
                        {
                            Id = 50,
                            CostPerDay = 18m,
                            Fee = 0m
                        });
                });

            modelBuilder.Entity("Moto.Domain.Entities.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("CourierId")
                        .HasColumnType("integer");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("ExpectedEndDate")
                        .HasColumnType("date");

                    b.Property<int>("MotorcycleId")
                        .HasColumnType("integer");

                    b.Property<int>("PlanId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal?>("TotalPayment")
                        .HasPrecision(7, 2)
                        .HasColumnType("numeric(7,2)");

                    b.HasKey("Id");

                    b.HasIndex("CourierId");

                    b.HasIndex("MotorcycleId");

                    b.HasIndex("PlanId");

                    b.ToTable("Rental");
                });

            modelBuilder.Entity("Moto.Domain.Entities.Courier", b =>
                {
                    b.OwnsOne("Moto.Domain.ValueObjects.Cnh", "DrivingLicense", b1 =>
                        {
                            b1.Property<int>("CourierId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("character varying(11)")
                                .HasColumnName("DrivingLicense");

                            b1.HasKey("CourierId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Courier");

                            b1.WithOwner()
                                .HasForeignKey("CourierId");
                        });

                    b.OwnsOne("Moto.Domain.ValueObjects.Cnpj", "Cnpj", b1 =>
                        {
                            b1.Property<int>("CourierId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(14)
                                .HasColumnType("character varying(14)")
                                .HasColumnName("Cnpj");

                            b1.HasKey("CourierId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Courier");

                            b1.WithOwner()
                                .HasForeignKey("CourierId");
                        });

                    b.Navigation("Cnpj")
                        .IsRequired();

                    b.Navigation("DrivingLicense")
                        .IsRequired();
                });

            modelBuilder.Entity("Moto.Domain.Entities.Motorcycle", b =>
                {
                    b.OwnsOne("Moto.Domain.ValueObjects.LicensePlate", "LicensePlate", b1 =>
                        {
                            b1.Property<int>("MotorcycleId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(8)
                                .HasColumnType("character varying(8)")
                                .HasColumnName("LicensePlate");

                            b1.HasKey("MotorcycleId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Motorcycle");

                            b1.WithOwner()
                                .HasForeignKey("MotorcycleId");
                        });

                    b.Navigation("LicensePlate")
                        .IsRequired();
                });

            modelBuilder.Entity("Moto.Domain.Entities.Rental", b =>
                {
                    b.HasOne("Moto.Domain.Entities.Courier", "Courier")
                        .WithMany()
                        .HasForeignKey("CourierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Moto.Domain.Entities.Motorcycle", "Motorcycle")
                        .WithMany()
                        .HasForeignKey("MotorcycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Moto.Domain.Entities.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courier");

                    b.Navigation("Motorcycle");

                    b.Navigation("Plan");
                });
#pragma warning restore 612, 618
        }
    }
}
