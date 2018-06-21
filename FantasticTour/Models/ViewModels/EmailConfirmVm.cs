namespace FantasticTour.Models.ViewModels
{
    public class EmailConfirmVm
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

    public class HotelVm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int CiudadId { get; set; }
    }
}