namespace Renetdux.Controllers
{
    public class ErrorViewModel
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public ErrorViewModel(string code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }
    }
}
