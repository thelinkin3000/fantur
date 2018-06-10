using System.Linq;
using System.Threading.Tasks;
using FantasticTour.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class PaquetesController : Controller
    {
        private readonly DataContext _context;

        public PaquetesController(DataContext context)
        {
            _context = context;
        }

        [Route("/api/[controller]")]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_context.Paquetes
                .Include(p => p.Atraccion)
                .Include(p => p.Estadia)
                .Include(p => p.Transporte));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_context.Paquetes
                .Include(p => p.Atraccion)
                .Include(p => p.Estadia)
                .Include(p => p.Transporte)
                .FirstOrDefault(a => a.Id == id));
        }

        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Save([FromBody] Paquete paquete)
        {
            if (paquete.Id != 0)
            {
                if (_context.Paquetes.AsNoTracking().FirstOrDefault(p => p.Id == paquete.Id) == null)
                {
                    return new OkObjectResult(new { error = "Se quiere guardar un paquete que no existe." });
                }
                _context.Paquetes.Update(paquete);
            }
            else
            {
                _context.Paquetes.Add(paquete);
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(paquete);
        }
    }
}