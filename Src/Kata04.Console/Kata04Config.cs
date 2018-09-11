using Microsoft.Extensions.Configuration;

namespace Kata04
{
    public class Kata04Config : IKata04Config
    {
        private readonly IConfiguration _configuration;

        public Kata04Config()
        {
            _configuration = LoadConfiguration();
        }

        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("applicationsettings.json", false, true);
            return builder.Build();
        }

        public string KataDataBaseAddress => _configuration?.GetValue<string>("appSettings:kataDataBaseAddress");
    }
}