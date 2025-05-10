using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stream.ViewModels.Dto;

namespace Stream.ViewModels.ViewModels;

public class GameViewModel
{
    public IEnumerable<GameDto> Games { get; set; } = new List<GameDto>();
    public GameDto Game { get; set; }
    public IEnumerable<SelectListItem> Platforms { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
    public string? SearchQuery { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalGames { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalGames / PageSize);

    public string PreviousPageUrl => CurrentPage > 1
        ? $"/Game/Index?searchQuery={SearchQuery}&pageNumber={CurrentPage - 1}&pageSize={PageSize}"
        : string.Empty;

    public string NextPageUrl => CurrentPage < TotalPages
        ? $"/Game/Index?searchQuery={SearchQuery}&pageNumber={CurrentPage + 1}&pageSize={PageSize}"
        : string.Empty;

    public Dictionary<string, string> DisplayNames { get; set; } = typeof(Stream.Models.Game)
        .GetProperties()
        .Where(p => p.Name is "Title" or "ReleaseDate" or "Platform" or "Genre")
        .ToDictionary(
            p => p.Name,
            p => p.GetCustomAttributes(typeof(DisplayAttribute), false)
                .Cast<DisplayAttribute>()
                .FirstOrDefault()?.Name ?? p.Name
        );
}