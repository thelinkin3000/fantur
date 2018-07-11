using System.Linq;
using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.Models.ViewModels;
using FantasticTour.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class TransportesController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapperService _mapperService;

        public TransportesController(DataContext context, IMapperService mapperService)
        {
            _context = context;
            _mapperService = mapperService;
        }

        [Route("/api/[controller]")]
        public IActionResult GetAll()
        {
            var result = _context.Transportes
                .Include(p => p.Destino)
                .ThenInclude(p => p.Pais)
                .Include(p => p.Origen)
                .ThenInclude(p => p.Pais)
                .Select(p => _mapperService.MapTransporte(p))
                .ToList();
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            var result = _context.Transportes
                .Include(p => p.Destino)
                .ThenInclude(p => p.Pais)
                .Include(p => p.Origen)
                .ThenInclude(p => p.Pais)
                .Select(p => _mapperService.MapTransporte(p))
                .FirstOrDefault(a => a.Id == id);
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
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