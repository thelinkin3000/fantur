using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FantasticTour.Migrations
{
    public partial class VueloToTransporte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paquetes_Vuelos_VueloId",
                table: "Paquetes");

            migrationBuilder.DropTable(
                name: "Vuelos");

            migrationBuilder.RenameColumn(
                name: "VueloId",
                table: "Paquetes",
                newName: "TransporteId");

            migrationBuilder.RenameIndex(
                name: "IX_Paquetes_VueloId",
                table: "Paquetes",
                newName: "IX_Paquetes_TransporteId");

            migrationBuilder.CreateTable(
                name: "Transporte",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Costo = table.Column<decimal>(nullable: false),
                    DestinoId = table.Column<int>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    OrigenId = table.Column<int>(nullable: true),
                    TipoTransporte = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transporte_Ciudades_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transporte_Ciudades_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transporte_DestinoId",
                table: "Transporte",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transporte_OrigenId",
                table: "Transporte",
                column: "OrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paquetes_Transporte_TransporteId",
                table: "Paquetes",
                column: "TransporteId",
                principalTable: "Transporte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paquetes_Transporte_TransporteId",
                table: "Paquetes");

            migrationBuilder.DropTable(
                name: "Transporte");

            migrationBuilder.RenameColumn(
                name: "TransporteId",
                table: "Paquetes",
                newName: "VueloId");

            migrationBuilder.RenameIndex(
                name: "IX_Paquetes_TransporteId",
                table: "Paquetes",
                newName: "IX_Paquetes_VueloId");

            migrationBuilder.CreateTable(
                name: "Vuelos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Costo = table.Column<decimal>(nullable: false),
                    DestinoId = table.Column<int>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    OrigenId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vuelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vuelos_Ciudades_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vuelos_Ciudades_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vuelos_DestinoId",
                table: "Vuelos",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vuelos_OrigenId",
                table: "Vuelos",
                column: "OrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paquetes_Vuelos_VueloId",
                table: "Paquetes",
                column: "VueloId",
                principalTable: "Vuelos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
