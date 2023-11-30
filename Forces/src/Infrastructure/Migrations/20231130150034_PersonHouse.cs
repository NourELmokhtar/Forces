using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forces.Infrastructure.Migrations
{
    public partial class PersonHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Room_RoomId",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Person",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_HouseId",
                table: "Person",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_House_HouseId",
                table: "Person",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Room_RoomId",
                table: "Person",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_House_HouseId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Room_RoomId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_HouseId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Room_RoomId",
                table: "Person",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
