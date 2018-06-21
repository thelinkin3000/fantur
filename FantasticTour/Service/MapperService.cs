using System.Threading;
using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.Models.ViewModels;
using FantasticTour.URF;

namespace FantasticTour.Service
{
    public class MapperService : IMapperService
    {
        private readonly IService<Ciudad> _ciudadService;
        private readonly IService<Hotel> _hotelService;

        public MapperService(IService<Ciudad> ciudadService, IService<Hotel> hotelService)
        {
            _ciudadService = ciudadService;
            _hotelService = hotelService;
        }


        public async Task<Hotel> MapHotel(HotelVm vm, Hotel hotel = null)
        {
            if (vm == null)
                return null;
            if (hotel == null)
                hotel = new Hotel();
            hotel.Direccion = vm.Direccion;
            hotel.Nombre = vm.Nombre;
            hotel.Telefono = vm.Telefono;
            Ciudad ciudad = await _ciudadService.FindAsync(vm.CiudadId, new CancellationToken());
            hotel.Ciudad = ciudad;
            return hotel;
        }

        public HotelVm MapHotel(Hotel hotel)
        {
            HotelVm result = new HotelVm();
            result.Id = hotel.Id;
            result.Direccion = hotel.Direccion;
            result.Nombre = hotel.Nombre;
            result.Telefono = hotel.Telefono;
            result.CiudadId = hotel.Ciudad?.Id ?? 0;
            return result;
        }
    }
}