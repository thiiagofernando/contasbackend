namespace CadastroConta.Api.ViewModels
{
    public class ReturnTokenViewModel
    {
        public string User { get; set; }
        public string NameUser { get; set; }
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
