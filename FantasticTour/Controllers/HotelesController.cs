using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.Repository;
using FantasticTour.URF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class HotelesController : Controller
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IService<Hotel> _hotelService;

        public HotelesController(DataContext context, IService<Hotel> hotelService)
        {
            _context = context;
            _hotelService = hotelService;
            _uow = new UnitOfWork(context);
        }

        [Route("/api/Hoteles")]
        public IActionResult Hoteles()
        {
            return new OkObjectResult(_context.Hoteles.Include(h => h.Ciudad).ThenInclude(c => c.Provincia).ThenInclude(p => p.Pais));
        }

        [Route("/api/Hoteles/{id}")]
        public IActionResult GetHotel(int id)
        {
            return new OkObjectResult(_context.Hoteles.FirstOrDefault(h => h.Id == id));
        }

        [HttpPost]
        [Route("/api/Hoteles")]
        public async Task<IActionResult> SaveHotel([FromBody] Hotel hotel)
        {
            Console.WriteLine(hotel);
            if (hotel.Id != 0)
            {
                Hotel exists = await _hotelService.FindAsync(hotel.Id, new CancellationToken()); 
                if (exists == null)
                {
                    return new BadRequestObjectResult(new {error = "Se quiere guardar un hotel que no existe."});
                }
                _hotelService.Detach(exists);
                _hotelService.Update(hotel);
            }
            else
            {
                _hotelService.Insert(hotel);
            }
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex.Message});
            }
            return new OkObjectResult(hotel);
        }
    }
}