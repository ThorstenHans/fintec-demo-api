namespace FintecDemo.API.Configuration
{
    public class CorsConfiguration
    {
        public bool Disable { get; set; }
        public bool AllowAnyHeader { get; set; }
        public bool AllowAnyOrigin { get; set; }
        public bool AllowAnyMethod { get; set; }
        public string[] Origins { get; set; }
        public string[] Headers { get; set; }
        public string[] Methods { get; set; }
    }
}
