using System;
using FantasticTour.Models.ViewModels;
using FantasticTour.Service;
using Microsoft.AspNetCore.Mvc;

namespace FantasticTour.Controllers
{
    public class MailingController : Controller
    {
        private readonly IMailingService _mailingService;

        public MailingController(IMailingService mailingService)
        {
            _mailingService = mailingService;
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public IActionResult SendMailing([FromBody] MailingVm vm)
        {
            if(!ModelState.IsValid)
                return new ObjectResult(new RequestResultVm(false, "Hay errores en el cuerpo del request"));
            try
            {
                _mailingService.FireSendMailing(vm.Titulo, vm.Cuerpo);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new RequestResultVm(false, ex.Message));
            }
            
            return new ObjectResult(new RequestResultVm(true));
        }
    }
}