namespace Swapfiets.TheftInspector.WebApi.V1.Models;

public sealed class GetBikeTheftsResponse(int count)
{
    public int Count { get; } = count;
}
