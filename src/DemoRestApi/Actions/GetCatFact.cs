using System.ComponentModel.DataAnnotations;
using DemoRestApi.Models;
using DemoRestApi.Services;
using FluentValidation;
using MediatR;

namespace DemoRestApi.Actions;

public class GetCatFact
{
   public class Request : IRequest<Response>
   {
      public int NumberOfFacts { get; set; } = 1;
   }

   public class Response
   {
      public IEnumerable<CatFact>? Facts { get; set; }
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

      public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
      {
         var facts = await _factApiClient.GetFactsAsync(request.NumberOfFacts).ConfigureAwait(false);
         return new Response {Facts = facts};
      }
   }

   public class RequestValidator : AbstractValidator<Request>
   {
      private const int MinFacts = 1;
      private const int MaxFacts = 5;

      public RequestValidator()
      {
         RuleFor(x => x.NumberOfFacts)
            .GreaterThan(10);

         // RuleFor(x => x.NumberOfFacts)
         //    .InclusiveBetween(MinFacts, MaxFacts);
      }
   }
}
