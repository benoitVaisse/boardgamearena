using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameArena.Migrations
{
    public partial class addTitlePart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Parts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Parts");
        }
    }
}
