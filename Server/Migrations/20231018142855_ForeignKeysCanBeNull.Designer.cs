﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using scrum_board_tool.Server.Model;

#nullable disable

namespace scrum_board_tool.Server.Migrations
{
    [DbContext(typeof(ScrumBoardDbContext))]
    [Migration("20231018142855_ForeignKeysCanBeNull")]
    partial class ForeignKeysCanBeNull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("scrum_board_tool.Shared.BacklogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Effort")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int?>("SprintId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.ToTable("BacklogItem");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.Sprint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprint");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.WorkTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BacklogItemId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TimeRemaining")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BacklogItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.BacklogItem", b =>
                {
                    b.HasOne("scrum_board_tool.Shared.Sprint", "Sprint")
                        .WithMany("BacklogItems")
                        .HasForeignKey("SprintId");

                    b.Navigation("Sprint");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.Sprint", b =>
                {
                    b.HasOne("scrum_board_tool.Shared.Project", "Project")
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.User", b =>
                {
                    b.HasOne("scrum_board_tool.Shared.Project", "Project")
                        .WithMany("Users")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.WorkTask", b =>
                {
                    b.HasOne("scrum_board_tool.Shared.BacklogItem", "BacklogItem")
                        .WithMany("Tasks")
                        .HasForeignKey("BacklogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scrum_board_tool.Shared.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId");

                    b.Navigation("BacklogItem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.BacklogItem", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.Project", b =>
                {
                    b.Navigation("Sprints");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.Sprint", b =>
                {
                    b.Navigation("BacklogItems");
                });

            modelBuilder.Entity("scrum_board_tool.Shared.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}