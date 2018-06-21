using System.Collections.Generic;
using FantasticTour.Models.ViewModels;

namespace FantasticTour.Service
{
    public interface IAutocompleteService
    {
        List<AutocompleteResultVm> AutocompletePais(string query);
        List<AutocompleteResultVm> AutocompleteCiudad(string query);
        List<AutocompleteResultVm> AutocompleteAtraccion(string query);
        List<AutocompleteResultVm> AutocompleteHotel(string query);
    }
}