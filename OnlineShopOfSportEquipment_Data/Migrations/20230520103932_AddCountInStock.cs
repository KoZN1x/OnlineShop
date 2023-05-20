using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopOfSportEquipment_Data.Migrations
{
    public partial class AddCountInStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountInStock",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountInStock",
                table: "Products");
        }
    }
}
