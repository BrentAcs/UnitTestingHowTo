using DemoRestApi.Actions;
using DemoRestApi.Models;

namespace DemoRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CatFactsController : ControllerBase
{
    private readonly ILogger<CatFactsController> _logger;
    private readonly IMediator _mediator;

    public CatFactsController(
       ILogger<CatFactsController> logger,
       IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("getSingle")]
    public async Task<ActionResult<CatFact>> Get()
    {
        var facts = await _mediator.Send(new GetCatFact.Request()).ConfigureAwait(false);
        var fact = facts.Facts.FirstOrDefault();

        return Ok(fact);
    }

    [HttpGet("getMany")]
    public async Task<ActionResult<IEnumerable<CatFact>>> GetFacts([FromQuery] GetCatFact.Request request)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var facts = await _mediator.Send(request).ConfigureAwait(false);

        return Ok(facts.Facts);
    }
}
