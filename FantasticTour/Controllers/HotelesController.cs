using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.Models.ViewModels;
using FantasticTour.Repository;
using FantasticTour.Service;
using FantasticTour.URF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FantasticTour.Controllers
{
    public class HotelesController : Controller
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IService<Hotel> _hotelService;
        private readonly IService<Ciudad> _ciudadService;
        private readonly IMapperService _mapperService;

        public HotelesController(DataContext context, IService<Hotel> hotelService, IService<Ciudad> ciudadService, IMapperService mapperService)
        {
            _context = context;
            _hotelService = hotelService;
            _ciudadService = ciudadService;
            _mapperService = mapperService;
            _uow = new UnitOfWork(context);
        }

        [Route("/api/[controller]")]
        public IActionResult GetAll()
        {
            List<HotelVm> result = _context.Hoteles
                .Include(h => h.Ciudad)
                .Select(h => _mapperService.MapHotel(h))
                .ToList();
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            HotelVm result = _mapperService.MapHotel(_context.Hoteles.Include(h => h.Ciudad)
                .FirstOrDefault(h => h.Id == id));
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Save([FromBody] HotelVm hotelVm)
        {
            Console.WriteLine(hotelVm);
            if (hotelVm.Id != 0)
            {
                Hotel hotel = await _hotelService.FindAsync(hotelVm.Id, new CancellationToken()); 
                if (hotel == null)
                {
                    return new OkObjectResult(new RequestResultVm(false, "Se quiere guardar un hotel que no existe."));
                }
                hotel = await _mapperService.MapHotel(hotelVm,hotel);
                if(hotelVm.CiudadId != 0 && hotel.Ciudad == null)
                    return new OkObjectResult(new RequestResultVm(false, $"No existe la ciudad con id {hotelVm.CiudadId}"));
                _hotelService.Update(hotel);
            }
            else
            {
                Hotel hotel = await _mapperService.MapHotel(hotelVm);
                if (hotelVm.CiudadId != 0 && hotel.Ciudad == null)
                    return new OkObjectResult(new RequestResultVm(false, $"No existe la ciudad con id {hotelVm.CiudadId}"));
                _hotelService.Insert(hotel);
            }
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new RequestResultVm(false, $"{ex.Message}"));
            }
            return new OkObjectResult(new RequestResultVm(true));
        }
    }
}