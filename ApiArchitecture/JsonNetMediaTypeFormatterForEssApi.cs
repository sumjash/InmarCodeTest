using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Perigee;
using RPCore.JsonSerialization;

namespace Jda.WfmEssApi
{
  public class JsonNetMediaTypeFormatterForEssApi : JsonNetMediaTypeFormatter
  {
    public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content,
     TransportContext transportContext)
    {
      JsonSerializer serializer = new RPJsonSerializer();
      serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

      return Task.Factory.StartNew(() =>
      {
        using (JsonTextWriter jsonTextWriter =
        new JsonTextWriter(new StreamWriter(stream, SelectCharacterEncoding(content.Headers))) { CloseOutput = false })
        {
          serializer.Serialize(jsonTextWriter, value);
          jsonTextWriter.Flush();
        }
      });
    }
  }
}