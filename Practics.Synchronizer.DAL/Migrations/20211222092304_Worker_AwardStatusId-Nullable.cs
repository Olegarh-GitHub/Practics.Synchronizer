using Microsoft.EntityFrameworkCore.Migrations;

namespace Practics.Synchronizer.DAL.Migrations
{
    public partial class Worker_AwardStatusIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_AwardStatuses_AwardStatusId",
                table: "Workers");

            migrationBuilder.AlterColumn<int>(
                name: "AwardStatusId",
                table: "Workers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_AwardStatuses_AwardStatusId",
                table: "Workers",
                column: "AwardStatusId",
                principalTable: "AwardStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_AwardStatuses_AwardStatusId",
                table: "Workers");

            migrationBuilder.AlterColumn<int>(
                name: "AwardStatusId",
                table: "Workers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_AwardStatuses_AwardStatusId",
                table: "Workers",
                column: "AwardStatusId",
                principalTable: "AwardStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
