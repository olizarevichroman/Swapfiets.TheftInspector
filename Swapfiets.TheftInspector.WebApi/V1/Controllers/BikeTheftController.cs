using Microsoft.AspNetCore.Mvc;
using Swapfiets.TheftInspector.BikeIndexAdapter;
using Swapfiets.TheftInspector.BikeIndexAdapter.Models;
using Swapfiets.TheftInspector.WebApi.V1.Models;

namespace Swapfiets.TheftInspector.WebApi.V1.Controllers;

[ApiController]
[Route("api/v1/thefts")]
public sealed class BikeTheftController(IBikeIndexApiClient client) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<GetBikeTheftsResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTheftsStatistic([FromQuery] GetTheftStatisticsClientRequest request)
    {
        var searchRequest = new BikeIndexSearchRequest(request.Distance, request.Location);
        var searchResponse = await client.SearchAsync(searchRequest);

        return searchResponse switch
        {
            BikeIndexSearchResponse.Success success => ToSuccessResult(success),
            BikeIndexSearchResponse.ServiceUnavailable => Problem(statusCode: StatusCodes.Status502BadGateway),
            BikeIndexSearchResponse.InternalError or _ => Problem(statusCode: StatusCodes.Status500InternalServerError),
        };

        OkObjectResult ToSuccessResult(BikeIndexSearchResponse.Success success)
        {
            var response = new GetBikeTheftsResponse(success.Proximity);

            return Ok(response);
        }
    }
}
