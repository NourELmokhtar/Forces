using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forces.Infrastructure.Migrations
{
    public partial class PersonRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Building_BuildingId",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Room",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "House",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Building",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Building_BuildingId",
                table: "Room",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Building_BuildingId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "House");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Building");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Building_BuildingId",
                table: "Room",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
