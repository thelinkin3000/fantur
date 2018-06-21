using FantasticTour.Models.ViewModels;
using FantasticTour.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FantasticTour.Controllers
{
    public class AutocompleteController : Controller
    {
        private readonly IAutocompleteService _autocompleteService;

        public AutocompleteController(IAutocompleteService autocompleteService)
        {
            _autocompleteService = autocompleteService;
        }

        [Route("/api/[controller]/[action]")]
        [HttpGet]
        public IActionResult Pais(string query)
        {
            var result = _autocompleteService.AutocompletePais(query);
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }

        [Route("/api/[controller]/[action]")]
        [HttpGet]
        public IActionResult Ciudad(string query)
        {
            var result = _autocompleteService.AutocompleteCiudad(query);
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }

        [Route("/api/[controller]/[action]")]
        [HttpGet]
        public IActionResult Hotel(string query)
        {
            var result = _autocompleteService.AutocompleteHotel(query);
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }

        [Route("/api/[controller]/[action]")]
        [HttpGet]
        public IActionResult Atraccion(string query)
        {
            var result = _autocompleteService.AutocompleteAtraccion(query);
            return new OkObjectResult(new RequestResultVm(true, Helpers.Serialize(result)));
        }



    }
}