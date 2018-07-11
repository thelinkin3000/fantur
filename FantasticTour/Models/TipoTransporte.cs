namespace FantasticTour.Models
{
    public enum TipoTransporte {
        Vuelo,
        Tren,
        Colectivo,
        Combi
    }

    public static class TipoTransporteExtensions
    {
        public static string GetName(this TipoTransporte tipoTransporte)
        {
            switch (tipoTransporte)
            {
                    case TipoTransporte.Colectivo:
                        return "Colectivo";
                    case TipoTransporte.Combi:
                        return "Combi";
                    case TipoTransporte.Tren:
                        return "Tren";
                    case TipoTransporte.Vuelo:
                        return "Vuelo";
                    default:
                        return "Inválido";

            }
        }

    }
}