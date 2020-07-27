using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameArena.Migrations
{
    public partial class fix6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartPlayer_Parts_PartId",
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

            migrationBuilder.RenameTable(
                name: "Play",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "PartPlayer",
                newName: "PartPlayers");

            migrationBuilder.RenameIndex(
                name: "IX_PartPlayer_PlayerId",
                table: "PartPlayers",
                newName: "IX_PartPlayers_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PartPlayer_PartId",
                table: "PartPlayers",
                newName: "IX_PartPlayers_PartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartPlayers",
                table: "PartPlayers",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartPlayers_Parts_PartId",
                table: "PartPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_PartPlayers_Players_PlayerId",
                table: "PartPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartPlayers",
                table: "PartPlayers");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Play");

            migrationBuilder.RenameTable(
                name: "PartPlayers",
                newName: "PartPlayer");

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
                name: "PK_PartPlayer",
                table: "PartPlayer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PartPlayer_Parts_PartId",
                table: "PartPlayer",
                column: "PartId",
                principalTable: "Parts",
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
    }
}
