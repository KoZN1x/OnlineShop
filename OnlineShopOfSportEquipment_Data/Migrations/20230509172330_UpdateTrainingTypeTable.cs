using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopOfSportEquipment_Data.Migrations
{
    public partial class UpdateTrainingTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TrainingTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingTypeId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_TrainingTypeId",
                table: "Products",
                column: "TrainingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_TrainingTypes_TrainingTypeId",
                table: "Products",
                column: "TrainingTypeId",
                principalTable: "TrainingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_TrainingTypes_TrainingTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TrainingTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TrainingTypeId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TrainingTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
