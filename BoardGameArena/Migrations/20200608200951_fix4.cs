using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameArena.Migrations
{
    public partial class fix4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartPlayers_Parts_PartId",
                table: "PartPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_PartPlayers_Players_PlayerId",
                table: "PartPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Games_GameId",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parts",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartPlayers",
                table: "PartPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Play");

            migrationBuilder.RenameTable(
                name: "Parts",
                newName: "Part");

            migrationBuilder.RenameTable(
                name: "PartPlayers",
                newName: "PartPlayer");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_GameId",
                table: "Part",
                newName: "IX_Part_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_PartPlayers_PlayerId",
                table: "PartPlayer",
                newName: "IX_PartPlayer_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PartPlayers_PartId",
                table: "PartPlayer",
                newName: "IX_PartPlayer_PartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Play",
                table: "Play",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Part",
                table: "Part",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartPlayer",
                table: "PartPlayer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Part_Game_GameId",
                table: "Part",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartPlayer_Part_PartId",
                table: "PartPlayer",
                column: "PartId",
                principalTable: "Part",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartPlayer_Play_PlayerId",
                table: "PartPlayer",
                column: "PlayerId",
                principalTable: "Play",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Part_Game_GameId",
                table: "Part");

            migrationBuilder.DropForeignKey(
                name: "FK_PartPlayer_Part_PartId",
                table: "PartPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_PartPlayer_Play_PlayerId",
                table: "PartPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Play",
                table: "Play");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartPlayer",
                table: "PartPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Part",
                table: "Part");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Play",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "PartPlayer",
                newName: "PartPlayers");

            migrationBuilder.RenameTable(
                name: "Part",
                newName: "Parts");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_PartPlayer_PlayerId",
                table: "PartPlayers",
                newName: "IX_PartPlayers_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PartPlayer_PartId",
                table: "PartPlayers",
                newName: "IX_PartPlayers_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_Part_GameId",
                table: "Parts",
                newName: "IX_Parts_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartPlayers",
                table: "PartPlayers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parts",
                table: "Parts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PartPlayers_Parts_PartId",
                table: "PartPlayers",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartPlayers_Players_PlayerId",
                table: "PartPlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Games_GameId",
                table: "Parts",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
