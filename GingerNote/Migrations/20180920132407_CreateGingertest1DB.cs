using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GingerNote.Migrations
{
    public partial class CreateGingertest1DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GingerNoteT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoteTitle = table.Column<string>(nullable: false),
                    NoteBody = table.Column<string>(nullable: true),
                    Pinned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GingerNoteT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistT",
                columns: table => new
                {
                    ChecklistId = table.Column<int>(nullable: false),
                    IsChecked = table.Column<bool>(nullable: false),
                    ChecklistName = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistT_GingerNoteT_Id",
                        column: x => x.Id,
                        principalTable: "GingerNoteT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistT");

            migrationBuilder.DropTable(
                name: "GingerNoteT");
        }
    }
}
