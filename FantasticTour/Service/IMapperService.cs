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
        TransporteVm MapHotel(Transporte transporte);
    }
}