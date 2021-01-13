using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations.Medicine
{
    public partial class UpdatedMedicine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count_Medicine",
                table: "Medicines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Medicines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count_Medicine",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Medicines");
        }
    }
}
