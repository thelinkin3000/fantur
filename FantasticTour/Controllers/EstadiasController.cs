using System;
using System.Linq;
using System.Threading.Tasks;
using FantasticTour.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class EstadiasController : Controller
    {
        private readonly DataContext _context;

        public EstadiasController(DataContext context)
        {
            _context = context;
        }

        [Route("/api/[controller]")]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_context.Estadias.Include(a => a.Hotel));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            Console.WriteLine(id);
            return new OkObjectResult(_context.Estadias.Include(a => a.Hotel).FirstOrDefault(a => a.Id == id));
        }

        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Save([FromBody] Estadia estadia)
        {
            if (estadia.Id != 0)
            {
                if (_context.Estadias.AsNoTracking().FirstOrDefault(a => a.Id == estadia.Id) == null)
                {
                    return new OkObjectResult(new {error = "Se quiere guardar una estadía que no existe."});
                }
                _context.Estadias.Update(estadia);
            }
            else
            {
                _context.Estadias.Add(estadia);
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(estadia);
        }
    }
}