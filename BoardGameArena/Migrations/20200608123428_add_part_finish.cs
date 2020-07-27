using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameArena.Migrations
{
    public partial class add_part_finish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinish",
                table: "Parts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinish",
                table: "Parts");
        }
    }
}
