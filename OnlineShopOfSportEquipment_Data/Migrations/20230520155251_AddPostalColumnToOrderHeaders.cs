using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopOfSportEquipment_Data.Migrations
{
    public partial class AddPostalColumnToOrderHeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "OrderHeaders");
        }
    }
}
