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
    public class AtraccionesController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapperService _mapperService;

        public AtraccionesController(DataContext context, IMapperService mapperService)
        {
            _context = context;
            _mapperService = mapperService;
        }

        [Route("/api/[controller]")]
        public IActionResult GetAll()
        {
            var result = _context
                .Atracciones
                .Include(a => a.Ciudad)
                .Select(a => _mapperService.MapAtraccion(a))
                .ToList();
            return new OkObjectResult(new RequestResultVm(true,Helpers.Serialize(result)));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            var result = _context.Atracciones
                .Include(a => a.Ciudad)
                .Select(a => _mapperService.MapAtraccion(a))
                .FirstOrDefault(a => a.Id == id);
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
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