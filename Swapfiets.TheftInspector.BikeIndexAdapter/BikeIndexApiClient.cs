using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Swapfiets.TheftInspector.BikeIndexAdapter.Models;

namespace Swapfiets.TheftInspector.BikeIndexAdapter;

internal sealed class BikeIndexApiClient(HttpClient client) : IBikeIndexApiClient
{
    public async Task<BikeIndexSearchResponse> SearchAsync(BikeIndexSearchRequest request)
    {
        var requestMessage = CreateHttpRequestMessage();

        try
        {
            var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);

            if (response.StatusCode >= HttpStatusCode.InternalServerError)
            {
                return new BikeIndexSearchResponse.ServiceUnavailable();
            }

            if (response.StatusCode >= HttpStatusCode.Ambiguous)
            {
                return new BikeIndexSearchResponse.InternalError();
            }

            var searchResponse = await response.Content.ReadFromJsonAsync<BikeIndexSearchResponse.Success?>();

            if (searchResponse is null)
            {
                return new BikeIndexSearchResponse.InternalError();
            }

            return searchResponse;
        }
        catch (Exception e) when (e is TaskCanceledException or HttpRequestException)
        {
            return new BikeIndexSearchResponse.ServiceUnavailable();
        }
        catch (Exception)
        {
            return new BikeIndexSearchResponse.InternalError();
        }

        HttpRequestMessage CreateHttpRequestMessage()
        {
            const string endpointUri = "https://bikeindex.org/api/v3/search/count";

            var query = QueryString
                .Create(name: "distance", value: request.Distance.ToString(CultureInfo.InvariantCulture))
                .Add(name: "location", value: request.Location);

            var requestUri = $"{endpointUri}{query.ToUriComponent()}";

            return new HttpRequestMessage(HttpMethod.Get, requestUri);
        }
    }
}
