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
        private readonly IService<Transporte> _transporteService;

        public MapperService(IService<Ciudad> ciudadService, IService<Hotel> hotelService, IService<Transporte> transporteService)
        {
            _ciudadService = ciudadService;
            _hotelService = hotelService;
            _transporteService = transporteService;
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

        public async Task<Transporte> MapTransporte(TransporteVm vm, Transporte transporte = null)
        {
            if (vm == null)
                return null;
            if (transporte == null)
                transporte = new Transporte();
            transporte.Fecha = vm.Fecha;
            transporte.Costo = vm.Costo;
            transporte.TipoTransporte = (TipoTransporte) vm.TipoTransporte;
            Ciudad origen = await _ciudadService.FindAsync(vm.OrigenId, new CancellationToken());
            transporte.Origen = origen;
            Ciudad destino = await _ciudadService.FindAsync(vm.DestinoId, new CancellationToken());
            transporte.Destino = destino;
            return transporte;
        }

        public TransporteVm MapHotel(Transporte transporte)
        {
            TransporteVm result = new TransporteVm();
            result.Id = transporte.Id;
            result.Fecha = transporte.Fecha;
            result.Costo = transporte.Costo;
            result.OrigenId = transporte.Origen?.Id ?? 0;
            result.DestinoId  = transporte.Destino?.Id ?? 0;
            return result;
        }
    }
}