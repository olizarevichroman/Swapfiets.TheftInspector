using Swapfiets.TheftInspector.BikeIndexAdapter.Models;

namespace Swapfiets.TheftInspector.BikeIndexAdapter;

public interface IBikeIndexApiClient
{
    Task<BikeIndexSearchResponse> SearchAsync(BikeIndexSearchRequest request);
}
