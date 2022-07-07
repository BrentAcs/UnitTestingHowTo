using System.Runtime.Serialization;

namespace DemoRestApi.Services;

[Serializable]
public class CatFactApiClientException : Exception
{
   //
   // For guidelines regarding the creation of new exception types, see
   //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
   // and
   //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
   //

   public CatFactApiClientException() { }
   public CatFactApiClientException(string message) : base(message) { }
   public CatFactApiClientException(string message, Exception inner) : base(message, inner) { }

   protected CatFactApiClientException(
      SerializationInfo info,
      StreamingContext context) : base(info, context)
   {
   }
}
