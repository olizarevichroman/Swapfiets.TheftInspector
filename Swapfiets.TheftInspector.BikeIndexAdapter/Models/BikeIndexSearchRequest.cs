namespace Swapfiets.TheftInspector.BikeIndexAdapter.Models;

public sealed class BikeIndexSearchRequest(int distance, string location)
{
    public int Distance { get; } = distance;

    public string Location { get; } = location;
}
