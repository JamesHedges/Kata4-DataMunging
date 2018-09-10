using MediatR;

namespace Kata04
{
    public class LoadTestDataCommand : IRequest<LoadTestDataResponse>
    {
        public string Source { get; set; }
        public string FilePath { get; set; }
    }
}

public class LoadTestDataResponse
{ }
