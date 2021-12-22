using Microsoft.EntityFrameworkCore.Migrations;

namespace Practics.Synchronizer.DAL.Migrations
{
    public partial class Worker_ChiefIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Workers_ChiefId",
                table: "Workers");

            migrationBuilder.AlterColumn<int>(
                name: "ChiefId",
                table: "Workers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Workers_ChiefId",
                table: "Workers",
                column: "ChiefId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Workers_ChiefId",
                table: "Workers");

            migrationBuilder.AlterColumn<int>(
                name: "ChiefId",
                table: "Workers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Workers_ChiefId",
                table: "Workers",
                column: "ChiefId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
