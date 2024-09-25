using Newtonsoft.Json;

namespace Moto.Api.Tests.Extensions;

internal static class HttpResponseMessageExtensions
{
    public async static Task<T> ParseTo<T>(this HttpResponseMessage response)
    {
        string reaponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(reaponse);
    }
}
