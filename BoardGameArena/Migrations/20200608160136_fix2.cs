using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameArena.Migrations
{
    public partial class fix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Games_GameID",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "GameID",
                table: "Parts",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_GameID",
                table: "Parts",
                newName: "IX_Parts_GameId");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Parts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Games_GameId",
                table: "Parts",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Games_GameId",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Parts",
                newName: "GameID");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_GameId",
                table: "Parts",
                newName: "IX_Parts_GameID");

            migrationBuilder.AlterColumn<int>(
                name: "GameID",
                table: "Parts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Games_GameID",
                table: "Parts",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
