using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasticTour.Migrations
{
    public partial class TrigramIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Creamos trigram indexes para hacer búsquedas hiper rápidas.
            //Si esta migración falla puede ser que los operadores trigram no estén instalados en la base de datos.
            //Para solucionar esto correr CREATE EXTENSION pg_trgm;
            migrationBuilder.Sql(@"CREATE INDEX ""IX_Ciudades_NombreTGR"" on ""Ciudades"" USING GIN(""Nombre"" gin_trgm_ops)");
            migrationBuilder.Sql(@"CREATE INDEX ""IX_Paises_NombreTGR"" on ""Paises"" USING GIN(""Nombre"" gin_trgm_ops)");
            migrationBuilder.Sql(@"CREATE INDEX ""IX_Hoteles_NombreTGR"" on ""Hoteles"" USING GIN(""Nombre"" gin_trgm_ops)");
            migrationBuilder.Sql(@"CREATE INDEX ""IX_Atracciones_NombreTGR"" on ""Atracciones"" USING GIN(""Nombre"" gin_trgm_ops)");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP INDEX ""IX_Ciudades_NombreTGR""");
            migrationBuilder.Sql(@"DROP INDEX ""IX_Paises_NombreTGR""");
            migrationBuilder.Sql(@"DROP INDEX ""IX_Hoteles_NombreTGR""");
            migrationBuilder.Sql(@"DROP INDEX ""IX_Atracciones_NombreTGR""");

        }
    }
}
