namespace FantasticTour.Models.ViewModels
{
    public class RequestResultVm
    {
        public RequestResultVm(bool valid, string message = "")
        {
            Valid = valid;
            Message = message;
        }
        public RequestResultVm(){ }


        public bool Valid { get; set; }
        public string Message { get; set; }
    }
}