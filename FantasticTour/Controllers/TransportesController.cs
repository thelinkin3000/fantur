using System.Linq;
using System.Threading.Tasks;
using FantasticTour.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class TransportesController : Controller
    {
        private readonly DataContext _context;

        public TransportesController(DataContext context)
        {
            _context = context;
        }

        [Route("/api/[controller]")]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_context.Transportes
                .Include(p => p.Destino)
                .ThenInclude(p => p.Pais)
                .Include(p => p.Origen)
                .ThenInclude(p => p.Pais));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_context.Transportes
                .Include(p => p.Destino)
                .ThenInclude(p => p.Pais)
                .Include(p => p.Origen)
                .ThenInclude(p => p.Pais)
                .FirstOrDefault(a => a.Id == id));
        }

        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Save([FromBody] Transporte transporte)
        {
            if (transporte.Id != 0)
            {
                if (_context.Transportes.AsNoTracking().FirstOrDefault(p => p.Id == transporte.Id) == null)
                {
                    return new OkObjectResult(new { error = "Se quiere guardar un transporte que no existe." });
                }
                _context.Transportes.Update(transporte);
            }
            else
            {
                _context.Transportes.Add(transporte);
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(transporte);
        }
    }
}