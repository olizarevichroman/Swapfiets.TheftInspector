namespace Swapfiets.TheftInspector.BikeIndexAdapter.Models;

public abstract class BikeIndexSearchResponse
{
    public sealed class Success(int proximity, int stolen, int non) : BikeIndexSearchResponse
    {
        public int Proximity { get; } = proximity;

        public int Stolen { get; } = stolen;

        public int Non { get; } = non;
    }

    public sealed class ServiceUnavailable : BikeIndexSearchResponse;

    public sealed class InternalError : BikeIndexSearchResponse;
}
