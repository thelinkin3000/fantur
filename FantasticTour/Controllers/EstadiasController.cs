using System;
using System.Linq;
using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.Models.ViewModels;
using FantasticTour.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class EstadiasController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapperService _mapperService;

        public EstadiasController(DataContext context, IMapperService mapperService)
        {
            _context = context;
            _mapperService = mapperService;
        }

        [Route("/api/[controller]")]
        public IActionResult GetAll()
        {
            var result = _context
                .Estadias
                .Include(a => a.Hotel)
                .ThenInclude(h => h.Ciudad)
                .Select(h => _mapperService.MapEstadia(h))
                .ToList();
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            var result = _context
                .Estadias
                .Include(a => a.Hotel)
                .ThenInclude(h => h.Ciudad)
                .Select(h => _mapperService.MapEstadia(h))
                .FirstOrDefault(a => a.Id == id);
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
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