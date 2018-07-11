using System;
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
        private readonly IService<Atraccion> _atraccionService;
        private readonly IService<Paquete> _paqueteService;
        private readonly IService<PaqueteContratado> _paqueteContratadoService;
        private readonly IService<Estadia> _estadiaService;

        public MapperService(IService<Ciudad> ciudadService, IService<Hotel> hotelService, IService<Transporte> transporteService, IService<Atraccion> atraccionService, IService<Paquete> paqueteService, IService<Estadia> estadiaService, IService<PaqueteContratado> paqueteContratadoService)
        {
            _ciudadService = ciudadService;
            _hotelService = hotelService;
            _atraccionService = atraccionService;
            _paqueteService = paqueteService;
            _estadiaService = estadiaService;
            _paqueteContratadoService = paqueteContratadoService;
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
            transporte.FechaIda = vm.FechaIda;
            transporte.FechaVuelta = vm.FechaVuelta;
            transporte.Costo = vm.Costo;
            transporte.TipoTransporte = (TipoTransporte)vm.TipoTransporteId;
            Ciudad origen = await _ciudadService.FindAsync(vm.OrigenId, new CancellationToken());
            transporte.Origen = origen;
            Ciudad destino = await _ciudadService.FindAsync(vm.DestinoId, new CancellationToken());
            transporte.Destino = destino;
            return transporte;
        }

        public TransporteVm MapTransporte(Transporte transporte)
        {
            TransporteVm result = new TransporteVm();
            result.Id = transporte.Id;
            result.FechaIda = transporte.FechaIda;
            result.FechaVuelta = transporte.FechaVuelta;
            result.Costo = transporte.Costo;
            result.OrigenId = transporte.Origen?.Id ?? 0;
            result.Origen = transporte.Origen?.Nombre ?? "-";
            result.Destino = transporte.Destino?.Nombre ?? "-";
            result.DestinoId  = transporte.Destino?.Id ?? 0;
            result.TipoTransporte = ((TipoTransporte)transporte.TipoTransporte).ToString();
            return result;
        }

        public async Task<Estadia> MapEstadia(EstadiaVm vm, Estadia estadia = null)
        {
            if (vm == null)
                return null;
            if (estadia == null)
                estadia = new Estadia();
            Hotel hotel = await _hotelService.FindAsync(vm.HotelId, new CancellationToken());
            estadia.Hotel = hotel;
            estadia.Costo = vm.Costo;
            estadia.FechaInicio = vm.FechaInicio;
            estadia.FechaFin = vm.FechaFin;
            return estadia;
        }

        public EstadiaVm MapEstadia(Estadia estadia)
        {
            if (estadia == null)
                return null;
            EstadiaVm result = new EstadiaVm();
            result.HotelId = estadia.Hotel?.Id ?? 0;
            result.Costo = estadia.Costo;
            result.FechaFin = estadia.FechaFin;
            result.FechaInicio = estadia.FechaInicio;
            result.Id = estadia.Id;
            result.Hotel = estadia.Hotel?.Nombre ?? "-";
            result.Ciudad = estadia.Hotel?.Ciudad?.Nombre ?? "-";
            return result;
        }

        public async Task<Atraccion> MapAtraccion(AtraccionVm vm, Atraccion atraccion = null)
        {
            if (vm == null)
                return null;
            if(atraccion == null)
                atraccion = new Atraccion();
            atraccion.Nombre = vm.Nombre;
            atraccion.Fecha = vm.Fecha;
            atraccion.Costo = vm.Costo;
            Ciudad ciudad = await _ciudadService.FindAsync(vm.CiudadId, new CancellationToken());
            atraccion.Ciudad = ciudad;
            return atraccion;
        }

        public AtraccionVm MapAtraccion(Atraccion atraccion)
        {
            if (atraccion == null)
                return null;
            AtraccionVm result = new AtraccionVm();
            result.Id = atraccion.Id;
            result.CiudadId = atraccion.Ciudad?.Id ?? 0;
            result.Costo = atraccion.Costo;
            result.Fecha = atraccion.Fecha;
            result.Nombre = atraccion.Nombre;
            result.Ciudad = atraccion.Ciudad?.Nombre ?? "-";
            return result;
        }

        public async Task<Paquete> MapPaquete(PaqueteVm vm, Paquete paquete = null)
        {
            if (vm == null)
                return null;
            if (paquete == null)
                paquete = new Paquete();
            paquete.Costo = vm.Costo;
            paquete.Disponible = vm.Disponible;
            paquete.FechaVencimiento = vm.FechaVencimiento;
            paquete.Nombre = vm.Nombre;
            Estadia estadia = await _estadiaService.FindAsync(vm.EstadiaId, new CancellationToken());
            Transporte transporte = await _transporteService.FindAsync(vm.TransporteId, new CancellationToken());
            Atraccion atraccion = await _atraccionService.FindAsync(vm.AtraccionId, new CancellationToken());
            paquete.Estadia = estadia;
            paquete.Transporte = transporte;
            paquete.Atraccion = atraccion;
            return paquete;
        }

        public PaqueteVm MapPaquete(Paquete paquete)
        {
            if (paquete == null)
                return null;
            PaqueteVm result = new PaqueteVm();
            result.Id = paquete.Id;
            result.Nombre = paquete.Nombre;
            result.Costo = paquete.Costo;
            result.Disponible = paquete.Disponible;
            result.FechaVencimiento = paquete.FechaVencimiento;
            result.AtraccionId = paquete.Atraccion?.Id ?? 0;
            result.EstadiaId = paquete.Estadia?.Id ?? 0;
            result.Hotel = paquete.Estadia?.Hotel?.Nombre ?? "-";
            result.Atraccion = paquete.Atraccion?.Nombre ?? "-";
            result.TransporteId = paquete.Transporte?.Id ?? 0;
            return result;
        }

    }


}