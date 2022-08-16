using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagementApplication.Migrations
{
    public partial class PriceNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Sale",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Sale",
                newName: "price");
        }
    }
}
