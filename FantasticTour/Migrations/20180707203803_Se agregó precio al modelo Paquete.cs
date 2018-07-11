using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasticTour.Migrations
{
    public partial class SeagregóprecioalmodeloPaquete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Costo",
                table: "Paquetes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Costo",
                table: "Paquetes");
        }
    }
}
