using DemoRestApi.Actions;
using DemoRestApi.Controllers;
using DemoRestApi.Models;

namespace DemoRestApi.Tests.XUnit;

public class CatFactsControllerTests
{
   private CatFactsController GetController(
      IMock<IMediator>? mediatorMock = null,
      IMock<ILogger<CatFactsController>>? loggerMock = null
   )
   {
      mediatorMock ??= new Mock<IMediator>();
      loggerMock ??= new Mock<ILogger<CatFactsController>>();

      return new CatFactsController(loggerMock.Object, mediatorMock.Object);
   }

   [Fact]
   public async Task Get_WillCallMediator_WithRequest()
   {
      // Arrange
      var mediatorMock = new Mock<IMediator>();
      mediatorMock
         .Setup(m =>
            m.Send(It.IsAny<GetCatFact.Request>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(new GetCatFact.Response());
      var sut = GetController(mediatorMock);

      // Act
      _ = await sut.Get();

      // Assert
      mediatorMock
         .Verify(m => m.Send(It.IsAny<GetCatFact.Request>(), It.IsAny<CancellationToken>()),
            Times.Once);
   }

   [Fact]
   public async Task GetFacts_WillReturn400_WhenRequestIsInvalid()
   {
      // Arrange
      var mediatorMock = new Mock<IMediator>();
      mediatorMock
         .Setup(m =>
            m.Send(It.IsAny<GetCatFact.Request>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(new GetCatFact.Response());
      var request = new GetCatFact.Request();
      var sut = GetController(mediatorMock);

      // To test validation FLOW, need to inject error into controller
      sut.ModelState.AddModelError("NumberOfFacts", "out of range.");

      // Act
      var response = await sut.GetFacts(request);
      var objectResult = response.Result as ObjectResult;

      // Assert
      objectResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
   }

   [Fact]
   public async Task GetFacts_WillReturnCatFact_WhenRequestIsValid()
   {
      // Arrange
      var testResponse = new GetCatFact.Response
      {
         Facts = new List<CatFact>
         {
            new CatFact
            {
               Fact = "sample fact",
               Length = 10
            }
         }
      };
      var mediatorMock = new Mock<IMediator>();
      mediatorMock
         .Setup(m =>
            m.Send(It.IsAny<GetCatFact.Request>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(testResponse);
      var request = new GetCatFact.Request();
      var sut = GetController(mediatorMock);

      // Act
      var response = await sut.GetFacts(request);
      var objectResult = response.Result as ObjectResult;
      var facts = objectResult.Value as IEnumerable<CatFact>;

      // Assert
      facts.Should().HaveCount(1);
   }
}
