﻿// <auto-generated />
using GingerNote.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GingerNote.Migrations
{
    [DbContext(typeof(GingerNoteContext))]
    [Migration("20180920132407_CreateGingertest1DB")]
    partial class CreateGingertest1DB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GingerNote.Models.Checklist", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("ChecklistId");

                    b.Property<string>("ChecklistName");

                    b.Property<bool>("IsChecked");

                    b.HasKey("Id");

                    b.ToTable("ChecklistT");
                });

            modelBuilder.Entity("GingerNote.Models.GingerNoteC", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NoteBody");

                    b.Property<string>("NoteTitle")
                        .IsRequired();

                    b.Property<bool>("Pinned");

                    b.HasKey("Id");

                    b.ToTable("GingerNoteT");
                });

            modelBuilder.Entity("GingerNote.Models.Checklist", b =>
                {
                    b.HasOne("GingerNote.Models.GingerNoteC")
                        .WithMany("NoteChecklist")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
