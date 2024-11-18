﻿// <auto-generated />
using System;
using Alcatraz.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alcatraz.Context.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20210814143040_FriendsUpdate")]
    partial class FriendsUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("Alcatraz.Context.Entities.PlayerStatisticsBoard", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BoardId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("TEXT");

                    b.Property<uint>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Score")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("PlayerStatisticBoards");
                });

            modelBuilder.Entity("Alcatraz.Context.Entities.PlayerStatisticsBoardValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("PlayerBoardId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RankingCriterionIndex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ScoreLostForNextSliceJSON")
                        .HasColumnType("TEXT");

                    b.Property<string>("SliceScoreJSON")
                        .HasColumnType("TEXT");

                    b.Property<string>("ValueJSON")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlayerBoardId");

                    b.ToTable("PlayerStatisticBoardValues");
                });

            modelBuilder.Entity("Alcatraz.Context.Entities.Relationship", b =>
                {
                    b.Property<uint>("User1Id")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("User2Id")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("ByRelationShip")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Details")
                        .HasColumnType("INTEGER");

                    b.HasKey("User1Id", "User2Id");

                    b.HasIndex("User2Id");

                    b.ToTable("UserRelationships");
                });

            modelBuilder.Entity("Alcatraz.Context.Entities.User", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerNickName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Alcatraz.Context.Entities.PlayerStatisticsBoardValue", b =>
                {
                    b.HasOne("Alcatraz.Context.Entities.PlayerStatisticsBoard", "PlayerBoard")
                        .WithMany("Values")
                        .HasForeignKey("PlayerBoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerBoard");
                });

            modelBuilder.Entity("Alcatraz.Context.Entities.Relationship", b =>
                {
                    b.HasOne("Alcatraz.Context.Entities.User", "User1")
                        .WithMany()
                        .HasForeignKey("User1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Alcatraz.Context.Entities.User", "User2")
                        .WithMany()
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("Alcatraz.Context.Entities.PlayerStatisticsBoard", b =>
                {
                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
