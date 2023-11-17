using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forces.Infrastructure.Migrations
{
    public partial class editOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasesId",
                table: "Office",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Office_BasesId",
                table: "Office",
                column: "BasesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Office_Bases_BasesId",
                table: "Office",
                column: "BasesId",
                principalTable: "Bases",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Office_Bases_BasesId",
                table: "Office");

            migrationBuilder.DropIndex(
                name: "IX_Office_BasesId",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "BasesId",
                table: "Office");
        }
    }
}
