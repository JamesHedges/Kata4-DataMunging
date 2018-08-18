using System;
using Microsoft.Extensions.Logging;

namespace Kata04
{
    class Kata06Application
    {
        private readonly ILogger _logger;

        public Kata06Application(ILogger logger)
        {
            _logger = logger;
            _logger.LogInformation("Logger injected");
        }

        public void OnExecute()
        {
            _logger.LogInformation("My fist log message");
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
