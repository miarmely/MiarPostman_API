using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class trying : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maids_Bosses_BossId",
                table: "Maids");

            migrationBuilder.AlterColumn<int>(
                name: "BossId",
                table: "Maids",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Maids_Bosses_BossId",
                table: "Maids",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maids_Bosses_BossId",
                table: "Maids");

            migrationBuilder.AlterColumn<int>(
                name: "BossId",
                table: "Maids",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maids_Bosses_BossId",
                table: "Maids",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
