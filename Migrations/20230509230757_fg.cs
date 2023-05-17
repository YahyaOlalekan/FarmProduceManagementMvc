using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmProduceManagement.Migrations
{
    public partial class fg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProduceName",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProduceId",
                table: "Products",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProduceId",
                table: "Products",
                column: "ProduceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Produces_ProduceId",
                table: "Products",
                column: "ProduceId",
                principalTable: "Produces",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Produces_ProduceId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProduceId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProduceId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProduceName",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
