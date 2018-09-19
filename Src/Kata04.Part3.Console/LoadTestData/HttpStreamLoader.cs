using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Kata04.Part3.LoadTestData
{
    public class HttpStreamLoader : IRequestHandler<LoadTestDataCommand, LoadTestDataResponse>
    {
        private readonly IKata04Config _configuration;

        public HttpStreamLoader(IKata04Config configuration)
        {
            _configuration = configuration;
        }

        private async Task<Stream> GetHttpStreamAsync(string source)
        {
            HttpClient client;
            client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration.KataDataBaseAddress)
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
