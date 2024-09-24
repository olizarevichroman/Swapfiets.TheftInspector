using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Swapfiets.TheftInspector.WebApi.V1.Models;

public sealed class GetTheftStatisticsClientRequest
{
    [BindRequired, Range(0, int.MaxValue)]
    public int Distance { get; set; }

    public string Location { get; set; }
}
