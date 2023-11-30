using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forces.Infrastructure.Migrations
{
    public partial class PersonInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Inventory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_PersonId",
                table: "Inventory",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Person_PersonId",
                table: "Inventory",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Person_PersonId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_PersonId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Inventory");
        }
    }
}
