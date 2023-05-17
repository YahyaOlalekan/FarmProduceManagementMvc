using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmProduceManagement.Migrations
{
    public partial class nnnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Transactions",
                newName: "TotalQuantity");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TransactionProduces",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "TransactionProduces",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "Produces",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CartItems",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ProduceId",
                table: "CartItems",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCostPrice",
                table: "CartItems",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProduceId",
                table: "CartItems",
                column: "ProduceId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Produces_ProduceId",
                table: "CartItems",
                column: "ProduceId",
                principalTable: "Produces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Users_UserId",
                table: "CartItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Produces_ProduceId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Users_UserId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProduceId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TransactionProduces");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TransactionProduces");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "Produces");

            migrationBuilder.DropColumn(
                name: "TotalCostPrice",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                table: "Transactions",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Transactions",
                newName: "Amount");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CartItems",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ProduceId",
                table: "CartItems",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
