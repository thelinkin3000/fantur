using System.Collections.Generic;
using System.Linq;
using System.Text;
using FantasticTour.Models;
using FantasticTour.Models.ViewModels;
using FantasticTour.URF;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.Service
{
    public class AutocompleteService : IAutocompleteService
    {
        private readonly IRepository<Pais> _paisRepository;
        private readonly IRepository<Ciudad> _ciudadRepository;
        private readonly IRepository<Hotel> _hotelRepository;
        private readonly IRepository<Atraccion> _atraccionRepository;
        private const int maxAutocomplete = 5;

        public AutocompleteService(IRepository<Pais> paisRepository, IRepository<Ciudad> ciudadRepository, IRepository<Hotel> hotelRepository, IRepository<Atraccion> atraccionRepository)
        {
            _paisRepository = paisRepository;
            _ciudadRepository = ciudadRepository;
            _hotelRepository = hotelRepository;
            _atraccionRepository = atraccionRepository;
        }
    
        public List<AutocompleteResultVm> AutocompletePais(string query)
        {
            List<string> keywords = query.Split(' ').ToList();
            StringBuilder builder = new StringBuilder("%");
            foreach (string keyword in keywords)
            {
                builder.Append(keyword + "%");
            }
            List<AutocompleteResultVm> result = _paisRepository
                .Table()
                .Where(p => EF.Functions.ILike(p.Nombre, builder.ToString()))
                .Select(p => new AutocompleteResultVm(){Text = p.Nombre, Value = p.Id})
                .Take(maxAutocomplete)
                .ToList();
            return result;
        }

        public List<AutocompleteResultVm> AutocompleteCiudad(string query)
        {
            List<string> keywords = query.Split(' ').ToList();
            StringBuilder builder = new StringBuilder("%");
            foreach (string keyword in keywords)
            {
                builder.Append(keyword + "%");
            }
            List<AutocompleteResultVm> result = _ciudadRepository
                .Table()
                .Include(p => p.Pais)
                .Where(p => EF.Functions.ILike(p.Nombre, builder.ToString()))
                .Select(p => new AutocompleteResultVm() { Text = $"{p.Nombre} ({p.Pais.Nombre})", Value = p.Id })
                .Take(maxAutocomplete)
                .ToList();
            return result;
        }

        public List<AutocompleteResultVm> AutocompleteAtraccion(string query)
        {
            List<string> keywords = query.Split(' ').ToList();
            StringBuilder builder = new StringBuilder("%");
            foreach (string keyword in keywords)
            {
                builder.Append(keyword + "%");
            }
            List<AutocompleteResultVm> result = _atraccionRepository
                .Table()
                .Where(p => EF.Functions.ILike(p.Nombre, builder.ToString()))
                .Select(p => new AutocompleteResultVm() { Text = p.Nombre, Value = p.Id })
                .Take(maxAutocomplete)
                .ToList();
            return result;
        }

        public List<AutocompleteResultVm> AutocompleteHotel(string query)
        {
            List<string> keywords = query.Split(' ').ToList();
            StringBuilder builder = new StringBuilder("%");
            foreach (string keyword in keywords)
            {
                builder.Append(keyword + "%");
            }
            List<AutocompleteResultVm> result = _hotelRepository
                .Table()
                .Where(p => EF.Functions.ILike(p.Nombre, builder.ToString()))
                .Select(p => new AutocompleteResultVm() { Text = p.Nombre, Value = p.Id })
                .Take(maxAutocomplete)
                .ToList();
            return result;
        }
    }
}