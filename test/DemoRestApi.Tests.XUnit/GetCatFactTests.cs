using DemoRestApi.Actions;
using DemoRestApi.Models;
using DemoRestApi.Services;

namespace DemoRestApi.Tests.XUnit;

public class GetCatFactTests
{
   // --- RequestValidator tests

   [Fact]
   public void Validate_ShouldBeSuccess()
   {
      // Arrange
      var validator = new GetCatFact.RequestValidator();
      var request = new GetCatFact.Request();

      // Act
      var result = validator.TestValidate(request);

      // Assert
      result.ShouldNotHaveAnyValidationErrors();
   }

   [Theory]
   [InlineData(1)]
   [InlineData(3)]
   [InlineData(5)]
   public void Validate_ShouldBeSuccess_ForNumberOfFacts(int numberOfFacts)
   {
      // Arrange
      var validator = new GetCatFact.RequestValidator();
      var request = new GetCatFact.Request {NumberOfFacts = numberOfFacts};

      // Act
      var result = validator.TestValidate(request);

      // Assert
      result.ShouldNotHaveAnyValidationErrors();
   }

   [Theory]
   [InlineData(0)]
   [InlineData(6)]
   public void Validate_ShouldBeInvalid_ForNumberOfFacts(int numberOfFacts)
   {
      // Arrange
      var validator = new GetCatFact.RequestValidator();
      var request = new GetCatFact.Request {NumberOfFacts = numberOfFacts};

      // Act
      var result = validator.TestValidate(request);

      // Assert
      result.ShouldHaveValidationErrorFor(p => p.NumberOfFacts);
   }

   // --- Handler

   private GetCatFact.Handler GetHandler(
      IMock<ICatFactApiClient> apiClientMock,
      IMock<ILogger<GetCatFact.Handler>>? loggerMock = null)
   {
      loggerMock ??= new Mock<ILogger<GetCatFact.Handler>>();

      return new GetCatFact.Handler(loggerMock.Object, apiClientMock.Object);
   }

   [Theory]
   [InlineData(1)]
   [InlineData(3)]
   public async Task Handle_WillReturnMatching_NumberOfFacts(int numberOfFacts)
   {
      // Arrange
      var testCatFact = new CatFact
      {
         Fact = "sample fact",
         Length = 10
      };
      var request = new GetCatFact.Request {NumberOfFacts = numberOfFacts};
      var apiClientMock = new Mock<ICatFactApiClient>();
      apiClientMock.Setup(m => m.GetFactAsync()).ReturnsAsync(testCatFact);
      var sut = GetHandler(apiClientMock);

      // Act
      var response = await sut.Handle(request, CancellationToken.None);

      // Assert
      response.Facts.Should().HaveCount(numberOfFacts);
   }
}
