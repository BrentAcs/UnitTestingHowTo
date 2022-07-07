using DemoRestApi.Models;

namespace DemoRestApi.Services;

public interface ICatFactApiClient
{
   Task<CatFact?> GetFactAsync();
}

public class CatFactApiClient : ICatFactApiClient
{
   private const string CatServiceUrlKey = "CatServiceUrl";

   private readonly IConfiguration _configuration;
   private readonly HttpClient _httpClient;

   public CatFactApiClient(IConfiguration configuration, HttpClient httpClient)
   {
      _configuration = configuration;
      _httpClient = httpClient;
   }

   public async Task<CatFact?> GetFactAsync()
   {
      var serviceUri = _configuration.GetValue<string>(CatServiceUrlKey);
      if (string.IsNullOrEmpty(serviceUri))
         throw new CatFactApiClientException($"Unable to find configuration key: '{CatServiceUrlKey}'.");

      var response = await _httpClient.GetAsync(serviceUri).ConfigureAwait(false);
      if (!response.IsSuccessStatusCode)
         return null;

      var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
      var fact = JsonSerializer.Deserialize<CatFact>(content, options);

      return fact;
   }
}
