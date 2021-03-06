﻿// <auto-generated />
using System;
using HeroTrainerApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HeroTrainerApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200925070603_TrainerClass")]
    partial class TrainerClass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HeroTrainerApp.API.Models.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ability");

                    b.Property<decimal>("CurrentPower");

                    b.Property<Guid>("GuidId");

                    b.Property<string>("Name");

                    b.Property<decimal>("StartingPower");

                    b.Property<string>("SuitColor");

                    b.Property<int>("TrainerId");

                    b.Property<DateTime>("TrainingStartDay");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("HeroTrainerApp.API.Models.Trainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("HeroTrainerApp.API.Models.Hero", b =>
                {
                    b.HasOne("HeroTrainerApp.API.Models.Trainer")
                        .WithMany("Heroes")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
