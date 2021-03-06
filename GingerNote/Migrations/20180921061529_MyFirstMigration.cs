﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GingerNote.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GingerNoteT",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoteTitle = table.Column<string>(nullable: false),
                    NoteBody = table.Column<string>(nullable: true),
                    Pinned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GingerNoteT", x => x.NoteId);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistT",
                columns: table => new
                {
                    ChecklistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsChecked = table.Column<bool>(nullable: false),
                    ChecklistName = table.Column<string>(nullable: true),
                    NoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistT", x => x.ChecklistId);
                    table.ForeignKey(
                        name: "FK_ChecklistT_GingerNoteT_NoteId",
                        column: x => x.NoteId,
                        principalTable: "GingerNoteT",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabelT",
                columns: table => new
                {
                    LabelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LabelName = table.Column<string>(nullable: true),
                    NoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelT", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_LabelT_GingerNoteT_NoteId",
                        column: x => x.NoteId,
                        principalTable: "GingerNoteT",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistT_NoteId",
                table: "ChecklistT",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelT_NoteId",
                table: "LabelT",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistT");

            migrationBuilder.DropTable(
                name: "LabelT");

            migrationBuilder.DropTable(
                name: "GingerNoteT");
        }
    }
}
