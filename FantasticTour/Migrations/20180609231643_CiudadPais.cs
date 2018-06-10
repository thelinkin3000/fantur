using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FantasticTour.Migrations
{
    public partial class CiudadPais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ciudades_Provincias_ProvinciaId",
                table: "Ciudades");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.RenameColumn(
                name: "ProvinciaId",
                table: "Ciudades",
                newName: "PaisId");

            migrationBuilder.RenameIndex(
                name: "IX_Ciudades_ProvinciaId",
                table: "Ciudades",
                newName: "IX_Ciudades_PaisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ciudades_Paises_PaisId",
                table: "Ciudades",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ciudades_Paises_PaisId",
                table: "Ciudades");

            migrationBuilder.RenameColumn(
                name: "PaisId",
                table: "Ciudades",
                newName: "ProvinciaId");

            migrationBuilder.RenameIndex(
                name: "IX_Ciudades_PaisId",
                table: "Ciudades",
                newName: "IX_Ciudades_ProvinciaId");

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(nullable: true),
                    PaisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provincias_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provincias_PaisId",
                table: "Provincias",
                column: "PaisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ciudades_Provincias_ProvinciaId",
                table: "Ciudades",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
