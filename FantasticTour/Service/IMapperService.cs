using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.Models.ViewModels;

namespace FantasticTour.Service
{
    public interface IMapperService
    {
        Task<Hotel> MapHotel(HotelVm vm, Hotel hotel = null);
        HotelVm MapHotel(Hotel hotel);

        Task<Transporte> MapTransporte(TransporteVm vm, Transporte transporte = null);
        TransporteVm MapTransporte(Transporte transporte);
        Task<Estadia> MapEstadia(EstadiaVm vm, Estadia estadia = null);
        EstadiaVm MapEstadia(Estadia estadia);
        Task<Atraccion> MapAtraccion(AtraccionVm vm, Atraccion atraccion = null);
        AtraccionVm MapAtraccion(Atraccion atraccion);
        Task<Paquete> MapPaquete(PaqueteVm vm, Paquete paquete = null);
        PaqueteVm MapPaquete(Paquete paquete);
    }
}