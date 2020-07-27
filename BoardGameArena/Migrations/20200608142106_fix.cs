using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameArena.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Games_JeuID",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_JeuID",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "JeuID",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "Parts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_GameID",
                table: "Parts",
                column: "GameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Games_GameID",
                table: "Parts",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Games_GameID",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_GameID",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "JeuID",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_JeuID",
                table: "Parts",
                column: "JeuID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Games_JeuID",
                table: "Parts",
                column: "JeuID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
