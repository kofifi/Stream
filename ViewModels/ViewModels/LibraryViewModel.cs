using System.ComponentModel.DataAnnotations;
using Stream.ViewModels.Dto;

namespace Stream.ViewModels.ViewModels;

public class LibraryViewModel
{
    public IEnumerable<LibraryDto> Libraries { get; set; } = new List<LibraryDto>();

    public string? SearchQuery { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalLibraries { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalLibraries / PageSize);

    public string PreviousPageUrl => CurrentPage > 1
        ? $"/Library/Index?searchQuery={SearchQuery}&pageNumber={CurrentPage - 1}&pageSize={PageSize}"
        : string.Empty;

    public string NextPageUrl => CurrentPage < TotalPages
        ? $"/Library/Index?searchQuery={SearchQuery}&pageNumber={CurrentPage + 1}&pageSize={PageSize}"
        : string.Empty;

    public Dictionary<string, string> DisplayNames { get; set; } = typeof(Stream.Models.Library)
        .GetProperties()
        .Where(p => p.Name is "User" or "Game" or "Status")
        .ToDictionary(
            p => p.Name,
            p => p.GetCustomAttributes(typeof(DisplayAttribute), false)
                .Cast<DisplayAttribute>()
                .FirstOrDefault()?.Name ?? p.Name
        );
}