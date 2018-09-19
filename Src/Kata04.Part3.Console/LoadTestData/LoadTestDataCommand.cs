using MediatR;

namespace Kata04.Part3.LoadTestData
{
    public class LoadTestDataCommand : IRequest<LoadTestDataResponse>
    {
        public string Source { get; set; }
        public string FilePath { get; set; }
    }

    public class LoadTestDataResponse
    { }
}