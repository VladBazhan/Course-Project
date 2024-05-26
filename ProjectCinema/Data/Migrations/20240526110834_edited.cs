using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCinema.Migrations
{
    /// <inheritdoc />
    public partial class edited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Ticket");

            migrationBuilder.AddColumn<string>(
                name: "FilmName",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_FilmId",
                table: "Sessions",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Films_FilmId",
                table: "Sessions",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Films_FilmId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_FilmId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "FilmName",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
