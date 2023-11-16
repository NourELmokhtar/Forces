using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forces.Infrastructure.Migrations
{
    public partial class AddItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItemBridge_InventoryItem_InventoryItemId",
                table: "InventoryItemBridge");

            migrationBuilder.RenameColumn(
                name: "InventoryItemId",
                table: "InventoryItemBridge",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryItemBridge_InventoryItemId",
                table: "InventoryItemBridge",
                newName: "IX_InventoryItemBridge_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItemBridge_Items_ItemId",
                table: "InventoryItemBridge",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItemBridge_Items_ItemId",
                table: "InventoryItemBridge");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "InventoryItemBridge",
                newName: "InventoryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryItemBridge_ItemId",
                table: "InventoryItemBridge",
                newName: "IX_InventoryItemBridge_InventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItemBridge_InventoryItem_InventoryItemId",
                table: "InventoryItemBridge",
                column: "InventoryItemId",
                principalTable: "InventoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
