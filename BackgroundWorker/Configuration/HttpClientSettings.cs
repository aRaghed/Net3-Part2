namespace BackgroundWorker.Configuration
{
    public class HttpClientSettings
    {
        public const string Section = "HttpClientSettings";

        public string Url { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}