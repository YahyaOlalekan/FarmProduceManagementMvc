using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmProduceManagement.Migrations
{
    public partial class fg2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "TransactionProduces");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TransactionProduces");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Transactions",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Produces",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Produces");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
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
        }
    }
}
