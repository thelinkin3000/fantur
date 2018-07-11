using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasticTour.Migrations
{
    public partial class TransportesFechas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Transporte",
                newName: "FechaVuelta");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaIda",
                table: "Transporte",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaIda",
                table: "Transporte");

            migrationBuilder.RenameColumn(
                name: "FechaVuelta",
                table: "Transporte",
                newName: "Fecha");
        }
    }
}
