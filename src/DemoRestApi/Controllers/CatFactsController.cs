using DemoRestApi.Actions;
using DemoRestApi.Models;
using DemoRestApi.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CatFactsController : ControllerBase
{
   private readonly ILogger<CatFactsController> _logger;
   private readonly ICatFactApiClient _factApiClient;
   private readonly IMediator _mediator;

   public CatFactsController(ILogger<CatFactsController> logger, ICatFactApiClient factApiClient, IMediator mediator)
   {
      _logger = logger;
      _factApiClient = factApiClient;
      _mediator = mediator;
   }

   [HttpGet("getSingle")]
   public async Task<ActionResult<CatFact>> Get()
   {
      var fact = await _factApiClient.GetFactAsync().ConfigureAwait(false);
      if (fact is null)
         return Problem();
   
      return Ok(fact);
   }
   
   [HttpGet("getMany")]
   public async Task<ActionResult<IEnumerable<CatFact>>> GetFacts(int maxFacts)
   {
      var facts = await _mediator
         .Send(new GetCatFact.Request {NumberOfFacts = maxFacts})
         .ConfigureAwait(false); 
      
      return Ok(facts.Facts);
   }
}
