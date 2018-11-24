namespace FintecDemo.API.Configuration
{
    public class CorsConfiguration
    {
        public bool Disable { get; set; } = false;
        public bool AllowAnyHeader { get; set; } = true;
        public bool AllowAnyOrigin { get; set; } = true;
        public bool AllowAnyMethod { get; set; } = true;
        public string[] Origins { get; set; }
        public string[] Headers { get; set; }
        public string[] Methods { get; set; }
    }
}
