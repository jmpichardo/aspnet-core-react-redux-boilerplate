namespace Renetdux.Infrastructure.Common
{
    public class Configuration
    {
        public string AppId { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public string JwtSecret { get; set; }
    }

    public class ConnectionStrings
    {
        public string DatabaseContext { get; set; }
    }
}
