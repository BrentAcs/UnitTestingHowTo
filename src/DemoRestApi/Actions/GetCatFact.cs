using DemoRestApi.Models;
using DemoRestApi.Services;

namespace DemoRestApi.Actions;

public class GetCatFact
{
    private const int MinFacts = 1;
    private const int MaxFacts = 5;

    [ExcludeFromCodeCoverage]
    public class Request : IRequest<Response>
    {
        public int NumberOfFacts { get; set; } = 1;
    }

    [ExcludeFromCodeCoverage]
    public class Response
    {
        public IList<CatFact> Facts { get; set; } = new List<CatFact>();
    }

    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly ILogger<Handler> _logger;
        private readonly ICatFactApiClient _factApiClient;

        public Handler(ILogger<Handler> logger, ICatFactApiClient factApiClient)
        {
            _logger = logger;
            _factApiClient = factApiClient;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken=default)
        {
            var response = new Response();
                
            for(int i = 0; i < request.NumberOfFacts; i++)
            {
                var fact = await _factApiClient.GetFactAsync().ConfigureAwait(false);
                response.Facts.Add(fact);
            }

            return response;
        }
    }

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.NumberOfFacts)
               .InclusiveBetween(MinFacts, MaxFacts);
        }
    }
}
