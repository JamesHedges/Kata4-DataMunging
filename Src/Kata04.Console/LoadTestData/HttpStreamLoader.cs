using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using MediatR;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace Kata04
{
    public class HttpStreamLoader : IRequestHandler<LoadTestDataCommand, LoadTestDataResponse>
    {
        private readonly IConfiguration _configuration;

        public HttpStreamLoader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<Stream> GetHttpStreamAsync(string source)
        {
            HttpClient client;
            client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration.GetValue<string>("appsettings:kataDataBaseAddress"))
            };

            var response = await client.GetStreamAsync(source);
            return response;
        }

        public async Task<LoadTestDataResponse> Handle(LoadTestDataCommand request, CancellationToken cancellationToken)
        {
            using (var outStream = File.Create(request.FilePath))
            using (Stream inStream = await GetHttpStreamAsync(request.Source))
            {
                inStream.CopyTo(outStream);
            }
            return new LoadTestDataResponse();
        }
    }
}
