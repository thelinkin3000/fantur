namespace FantasticTour.Service
{
    public interface IMailingService
    {
        bool FireSendMailing(string titulo, string cuerpo);
    }
}