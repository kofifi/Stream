using System.ComponentModel.DataAnnotations;

namespace Stream.Models
{
    public class Library
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        // Remove 'required' here
        public User? User { get; set; }

        [Required]
        public int GameId { get; set; }

        // Remove 'required' here
        public Game? Game { get; set; }

        [Required]
        public string? Status { get; set; }
    }

}
