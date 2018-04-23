using System;
using System.Linq;
using System.Threading.Tasks;
using FantasticTour.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class HotelesController : Controller
    {
        private readonly DataContext _context;

        public HotelesController(DataContext context)
        {
            _context = context;
        }

        [Route("/api/Hoteles")]
        public IActionResult Hoteles()
        {
            return new OkObjectResult(EntityFrameworkQueryableExtensions.Include<Hotel, Ciudad>(_context.Hoteles, h => h.Ciudad).ThenInclude(c => c.Provincia).ThenInclude(p => p.Pais));
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
                if (_context.Hoteles.AsNoTracking().FirstOrDefault(h => h.Id == hotel.Id) == null)
                {
                    return new BadRequestObjectResult(new {error = "Se quiere guardar un hotel que no existe."});
                }
                _context.Hoteles.Update(hotel);
            }
            else
            {
                _context.Hoteles.Add(hotel);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex.Message});
            }
            return new OkObjectResult(hotel);
        }
    }
}