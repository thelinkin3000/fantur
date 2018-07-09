using System;
using System.Linq;
using System.Threading.Tasks;
using FantasticTour.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class AtraccionesController : Controller
    {
        private readonly DataContext _context;

        public AtraccionesController(DataContext context)
        {
            _context = context;
        }

        [Route("/api/[controller]")]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_context.Atracciones.Include(a => a.Ciudad));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            Console.WriteLine(id);
            return new OkObjectResult(_context.Atracciones.Include(a => a.Ciudad).FirstOrDefault(a => a.Id == id));
        }

        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Save([FromBody] Atraccion atraccion)
        {
            if (atraccion.Id != 0)
            {
                if (_context.Atracciones.AsNoTracking().FirstOrDefault(a => a.Id == atraccion.Id) == null)
                {
                    return new OkObjectResult(new {error = "Se quiere guardar una atracción que no existe."});
                }
                _context.Atracciones.Update(atraccion);
            }
            else
            {
                _context.Atracciones.Add(atraccion);
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(atraccion);
        }
    }
}