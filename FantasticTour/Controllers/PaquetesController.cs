using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.Models.ViewModels;
using FantasticTour.Service;
using FantasticTour.URF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;

namespace FantasticTour.Controllers
{
    public class PaquetesController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapperService _mapperService;
        private readonly IService<Hotel> _hotelesService;
        private readonly IService<Estadia> _estadiasService;
        private readonly IService<Ciudad> _ciudadesService;
        private readonly IService<Transporte> _transportesService;
        private readonly IService<Atraccion> _atraccionesService;
        public PaquetesController(DataContext context, IMapperService mapperService, IService<Hotel> hotelesService, IService<Estadia> estadiasService, IService<Transporte> transportesService, IService<Atraccion> atraccionesService, IService<Ciudad> ciudadesService)
        {
            _context = context;
            _mapperService = mapperService;
            _hotelesService = hotelesService;
            _estadiasService = estadiasService;
            _transportesService = transportesService;
            _atraccionesService = atraccionesService;
            _ciudadesService = ciudadesService;
        }

        [Route("/api/[controller]")]
        public IActionResult GetAll([FromQuery] FiltroVm filtros)
        {
            IQueryable<Paquete> paquetes = _context.Paquetes
                .Include(p => p.Atraccion)
                .Include(p => p.Estadia)
                .ThenInclude(e => e.Hotel)
                .Include(p => p.Transporte);
            if (filtros != null)
            {
                if (filtros.Vuelta != null)
                    paquetes = paquetes.Where(p => p.Atraccion.Fecha <= filtros.Vuelta);
                if (filtros.Ida != null)
                    paquetes = paquetes.Where(p => p.Atraccion.Fecha >= filtros.Ida);
                if (filtros.OrigenId != 0)
                    paquetes = paquetes.Where(p => p.Transporte.Origen.Id == filtros.OrigenId);
                if (filtros.DestinoId != 0)
                    paquetes = paquetes.Where(p => p.Transporte.Destino.Id == filtros.DestinoId);
            }
            List<PaqueteVm> result = paquetes.Select(p => _mapperService.MapPaquete(p)).ToList();
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            PaqueteVm result = _context.Paquetes
                .Include(p => p.Atraccion)
                .Include(p => p.Estadia)
                .ThenInclude(e => e.Hotel)
                .Include(p => p.Transporte)
                .Select(p => _mapperService.MapPaquete(p))
                .FirstOrDefault(a => a.Id == id);
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Save([FromBody] CrearPaqueteVm vm)
        {
            Paquete paquete = new Paquete();
            Estadia estadia = new Estadia();
            Transporte transporte = new Transporte();
            Hotel hotel = await _hotelesService.FindAsync(vm.HotelId, new CancellationToken());
            if (hotel == null)
                return  new OkObjectResult(new RequestResultVm(false, $"No existe el hotel con id {vm.HotelId}"));
            Atraccion atraccion = await _atraccionesService.FindAsync(vm.AtraccionId, new CancellationToken());
            if (atraccion == null)
                return  new OkObjectResult(new RequestResultVm(false, $"No existe la atraccion con id {vm.AtraccionId}"));
            Ciudad destino = await _ciudadesService.FindAsync(vm.DestinoId, new CancellationToken());
            Ciudad origen = await _ciudadesService.FindAsync(vm.OrigenId, new CancellationToken());
            if(destino == null || origen == null)
                return  new OkObjectResult(new RequestResultVm(false, $"Alguna de las ciudades no existe"));

            estadia.Hotel = hotel;
            estadia.Costo = vm.CostoEstadia;
            estadia.FechaInicio = vm.Ingreso;
            estadia.FechaFin= vm.Egreso;
            _context.Estadias.Add(estadia);
            transporte.TipoTransporte = TipoTransporte.Vuelo;
            transporte.Costo = vm.CostoTransporte;
            transporte.Destino = destino;
            transporte.Origen = origen;
            transporte.FechaIda = vm.Partida;
            transporte.FechaVuelta = vm.Egreso;
            _context.Transportes.Add(transporte);
            paquete.Estadia = estadia;
            paquete.Atraccion = atraccion;
            paquete.Disponible = true;
            paquete.Nombre = vm.Nombre;
            paquete.Transporte = transporte;
            paquete.FechaVencimiento = vm.FechaVencimiento;
            _context.Paquetes.Add(paquete);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new RequestResultVm(false, ex.Message));
            }
            
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(paquete)));
        }
    }

    public class FiltroVm
    {
        public DateTime? Ida { get; set; }
        public DateTime? Vuelta { get; set; }
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
    }
}