﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Practics.Synchronizer.DAL.Context;

namespace Practics.Synchronizer.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211222080830_Persons")]
    partial class Persons
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Practics.Synchronizer.Core.Models.AwardStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ExtKey")
                        .HasColumnType("text");

                    b.Property<int>("Hash")
                        .HasColumnType("integer");

                    b.Property<bool>("MarkedForCreate")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForUpdate")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExtKey")
                        .IsUnique();

                    b.ToTable("AwardStatuses");
                });

            modelBuilder.Entity("Practics.Synchronizer.Core.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Disabled")
                        .HasColumnType("boolean");

                    b.Property<string>("ExtKey")
                        .HasColumnType("text");

                    b.Property<int>("Hash")
                        .HasColumnType("integer");

                    b.Property<bool>("MarkedForCreate")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForUpdate")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExtKey")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Practics.Synchronizer.Core.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ExtKey")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<int>("Hash")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("MarkedForCreate")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForUpdate")
                        .HasColumnType("boolean");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExtKey")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Practics.Synchronizer.Core.Models.PersonContacts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("ExtKey")
                        .HasColumnType("text");

                    b.Property<int>("Hash")
                        .HasColumnType("integer");

                    b.Property<bool>("MarkedForCreate")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForUpdate")
                        .HasColumnType("boolean");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("WorkEmail")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExtKey")
                        .IsUnique();

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("PersonContacts");
                });

            modelBuilder.Entity("Practics.Synchronizer.Core.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AwardStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("ChiefId")
                        .HasColumnType("integer");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("ExtKey")
                        .HasColumnType("text");

                    b.Property<bool>("Fired")
                        .HasColumnType("boolean");

                    b.Property<int>("Hash")
                        .HasColumnType("integer");

                    b.Property<bool>("MarkedForCreate")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("MarkedForUpdate")
                        .HasColumnType("boolean");

                    b.Property<bool>("MaternityLeave")
                        .HasColumnType("boolean");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<string>("WorkerNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AwardStatusId");

                    b.HasIndex("ChiefId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ExtKey")
                        .IsUnique();

                    b.HasIndex("PersonId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("Practics.Synchronizer.Core.Models.PersonContacts", b =>
                {
                    b.HasOne("Practics.Synchronizer.Core.Models.Person", "Person")
                        .WithOne("Contacts")
                        .HasForeignKey("Practics.Synchronizer.Core.Models.PersonContacts", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Practics.Synchronizer.Core.Models.Worker", b =>
                {
                    b.HasOne("Practics.Synchronizer.Core.Models.AwardStatus", "AwardStatus")
                        .WithMany()
                        .HasForeignKey("AwardStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Practics.Synchronizer.Core.Models.Worker", "Chief")
                        .WithMany()
                        .HasForeignKey("ChiefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Practics.Synchronizer.Core.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Practics.Synchronizer.Core.Models.Person", "Person")
                        .WithMany("Workers")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AwardStatus");

                    b.Navigation("Chief");

                    b.Navigation("Department");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Practics.Synchronizer.Core.Models.Person", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Workers");
                });
#pragma warning restore 612, 618
        }
    }
}
