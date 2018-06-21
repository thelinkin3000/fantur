using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FantasticTour.Models;
using FantasticTour.Models.ViewModels;
using FantasticTour.URF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Controllers
{
    public class CiudadesController : Controller
    {
        private readonly DataContext _context;
        private readonly IService<Pais> _paisService;
        private readonly IService<Ciudad> _ciudadService;

        public CiudadesController(DataContext context, IService<Pais> paisService, IService<Ciudad> ciudadService)
        {
            _context = context;
            _paisService = paisService;
            _ciudadService = ciudadService;
        }

        [Route("/api/[controller]")]
        public async Task<IActionResult> GetByPais(int idPais)
        {
            bool paisExists = await _paisService.ExistsAsync(idPais, new CancellationToken());
            if (!paisExists)
            {
                return new OkObjectResult(new {error = $"No existe el país con id {idPais}"});
            }

            Pais pais = await _paisService.FindAsync(idPais, new CancellationToken());

            return new OkObjectResult(_context.Ciudades.Include(c => c.Pais).Where(c => c.Pais == pais));
        }

        [Route("/api/[controller]/{id}")]
        public IActionResult Get(int id)
        {

            return new OkObjectResult(new RequestResultVm(true,Helpers.Serialize(_context.Ciudades
                .FirstOrDefault(a => a.Id == id))));
        }

        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Save([FromBody] Ciudad ciudad)
        {
            if (ciudad.Id != 0)
            {
                if (_context.Ciudades.AsNoTracking().FirstOrDefault(p => p.Id == ciudad.Id) == null)
                {
                    return new OkObjectResult(new { error = "Se quiere guardar un paquete que no existe." });
                }
                _context.Ciudades.Update(ciudad);
            }
            else
            {
                _context.Ciudades.Add(ciudad);
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(ciudad);
        }
    }
}