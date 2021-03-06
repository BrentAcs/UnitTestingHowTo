using DemoRestApi.Services;

namespace DemoRestApi.Tests.XUnit;

public class CatFactApiClientTests
{
   private const string SendAsyncMethodName = "SendAsync";

   private HttpClient GetHttpClient(string responsePayload, HttpStatusCode code = HttpStatusCode.OK)
   {
      var content = new StringContent(responsePayload);
      var response = new HttpResponseMessage
      {
         StatusCode = code,
         Content = content
      };

      var handlerMock = new Mock<HttpMessageHandler>();
      handlerMock.Protected()
         .Setup<Task<HttpResponseMessage>>(
            SendAsyncMethodName,
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
         )
         .ReturnsAsync(response)
         .Verifiable();

      return new HttpClient(handlerMock.Object);
   }

   private IConfiguration GetValideConfiguration()
   {
      var inMemorySettings = new Dictionary<string, string>
      {
         {"CatServiceUrl", "http://mock.url"},
      };

      IConfiguration configuration = new ConfigurationBuilder()
         .AddInMemoryCollection(inMemorySettings)
         .Build();
      return configuration;
   }

   // ---------------------------------------------------------------------------------------------
   // -- Happy paths
        
   [Fact]
   public async Task GetFactAsync_ShouldReturnNotNull_WhenConfiguredCorrectly()
   {
      // Arrange
      var apiResponsePayload = "{ \"fact\": \"sample fact\", \"length\": 11 }";
      var httpClient = GetHttpClient(apiResponsePayload, HttpStatusCode.OK);
      var configuration = GetValideConfiguration();
      var sut = new CatFactApiClient(configuration, httpClient);

      // Act
      var result = await sut.GetFactAsync();

      // Assert
      result.Should().NotBeNull();
   }

   [Fact]
   public async Task GetFactAsync_ShouldReturnTestFact_WhenConfiguredCorrectly()
   {
      // Arrange
      var apiResponsePayload = "{ \"fact\": \"sample fact\", \"length\": 11 }";
      var httpClient = GetHttpClient(apiResponsePayload, HttpStatusCode.OK);
      var configuration = GetValideConfiguration();
      var sut = new CatFactApiClient(configuration, httpClient);

      // Act
      var result = await sut.GetFactAsync();

      // Assert
      result!.Fact.Should().Be("sample fact");
   }
   
   // ---------------------------------------------------------------------------------------------
   // -- Unhappy paths

   [Fact]
   public async Task GetFactAsync_ShouldThrow_WhenMissingConfiguration()
   {
      // Arrange
      var apiResponsePayload = "{ \"fact\": \"sample fact\", \"length\": 11 }";
      var httpClient = GetHttpClient(apiResponsePayload, HttpStatusCode.OK);
      var configurationMock = new Mock<IConfiguration>();
      var sut = new CatFactApiClient(configurationMock.Object, httpClient);

      // Act
      var action = () => sut.GetFactAsync();

      // Assert
      action.Should()
         .ThrowAsync<CatFactApiClientException>()
         .WithMessage("Unable to find configuration key: 'CatServiceUrl'.");
   }

   [Fact]
   public async Task GetFactAsync_ShouldReturnNull_WhenApiCall404s()
   {
      // Arrange
      //var apiResponsePayload = "{ \"fact\": \"sample fact\", \"length\": 11 }";
      var apiResponsePayload = string.Empty;
      var httpClient = GetHttpClient(apiResponsePayload, HttpStatusCode.NotFound);
      var configuration = GetValideConfiguration();
      var sut = new CatFactApiClient(configuration, httpClient);

      // Act
      var result = await sut.GetFactAsync();

      // Assert
      result.Should().BeNull();
   }

}
