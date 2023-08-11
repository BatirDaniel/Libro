namespace Libro.Infrastructure.Persistence.SystemConfiguration.AppSettings
{
    public class AppSettings
    {
        public string? AdminPassword { get; set; }
        public string? Cookie_Name { get; set; }
        public int ExpireTimeSpan { get; set; }
        public bool SlidingExpiration { get; set; }
    }
}
