using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Repos.Data.Migrations
{
    public partial class BrandIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "productBrands",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_productBrands_Name",
                table: "productBrands",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_productBrands_Name",
                table: "productBrands");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "productBrands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
