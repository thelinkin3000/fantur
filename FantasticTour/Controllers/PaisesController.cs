using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.URF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class PaisesController : Controller
    {
        private readonly DataContext _context;
        private readonly IService<Pais> _paisService;

        public PaisesController(DataContext context, IService<Pais> paisService)
        {
            _context = context;
            _paisService = paisService;
        }

        [Route("/api/[controller]")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(_context.Paises);
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_context.Paises
                .FirstOrDefault(a => a.Id == id));
        }

        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Save([FromBody] Pais pais)
        {
            if (pais.Id != 0)
            {
                if (_context.Paises.AsNoTracking().FirstOrDefault(p => p.Id == pais.Id) == null)
                {
                    return new OkObjectResult(new { error = "Se quiere guardar un paquete que no existe." });
                }
                _context.Paises.Update(pais);
            }
            else
            {
                _context.Paises.Add(pais);
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(pais);
        }
    }
}