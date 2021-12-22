using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Practics.Synchronizer.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AwardStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Hash = table.Column<int>(type: "integer", nullable: false),
                    ExtKey = table.Column<string>(type: "text", nullable: true),
                    MarkedForCreate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Disabled = table.Column<bool>(type: "boolean", nullable: false),
                    Hash = table.Column<int>(type: "integer", nullable: false),
                    ExtKey = table.Column<string>(type: "text", nullable: true),
                    MarkedForCreate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    SecondName = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Hash = table.Column<int>(type: "integer", nullable: false),
                    ExtKey = table.Column<string>(type: "text", nullable: true),
                    MarkedForCreate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    WorkEmail = table.Column<string>(type: "text", nullable: true),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    Hash = table.Column<int>(type: "integer", nullable: false),
                    ExtKey = table.Column<string>(type: "text", nullable: true),
                    MarkedForCreate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonContacts_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkerNumber = table.Column<string>(type: "text", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    ChiefId = table.Column<int>(type: "integer", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    AwardStatusId = table.Column<int>(type: "integer", nullable: false),
                    Fired = table.Column<bool>(type: "boolean", nullable: false),
                    MaternityLeave = table.Column<bool>(type: "boolean", nullable: false),
                    Hash = table.Column<int>(type: "integer", nullable: false),
                    ExtKey = table.Column<string>(type: "text", nullable: true),
                    MarkedForCreate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedForDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_AwardStatuses_AwardStatusId",
                        column: x => x.AwardStatusId,
                        principalTable: "AwardStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Workers_ChiefId",
                        column: x => x.ChiefId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AwardStatuses_ExtKey",
                table: "AwardStatuses",
                column: "ExtKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ExtKey",
                table: "Departments",
                column: "ExtKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_ExtKey",
                table: "People",
                column: "ExtKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonContacts_ExtKey",
                table: "PersonContacts",
                column: "ExtKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonContacts_PersonId",
                table: "PersonContacts",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_AwardStatusId",
                table: "Workers",
                column: "AwardStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_ChiefId",
                table: "Workers",
                column: "ChiefId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_DepartmentId",
                table: "Workers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_ExtKey",
                table: "Workers",
                column: "ExtKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_PersonId",
                table: "Workers",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonContacts");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "AwardStatuses");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
