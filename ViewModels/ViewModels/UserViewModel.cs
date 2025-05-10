using System.ComponentModel.DataAnnotations;
using Stream.ViewModels.Dto;

namespace Stream.ViewModels.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();
        public string? SearchQuery { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalUsers { get; set; }
        
        public int TotalPages => (int)Math.Ceiling((double)TotalUsers / PageSize);
        
        public Dictionary<string, string> DisplayNames { get; set; } = typeof(Stream.Models.User)
            .GetProperties()
            .Where(p => p.Name == "Username" || p.Name == "Email" || p.Name == "CreatedAt")
            .ToDictionary(
                p => p.Name,
                p => p.GetCustomAttributes(typeof(DisplayAttribute), false)
                    .Cast<DisplayAttribute>()
                    .FirstOrDefault()?.Name ?? p.Name
            );
        
        public string PreviousPageUrl => CurrentPage > 1 
            ? $"/User/Index?searchQuery={SearchQuery}&pageNumber={CurrentPage - 1}&pageSize={PageSize}" 
            : string.Empty;

        public string NextPageUrl => CurrentPage < TotalPages 
            ? $"/User/Index?searchQuery={SearchQuery}&pageNumber={CurrentPage + 1}&pageSize={PageSize}" 
            : string.Empty;
    }
}