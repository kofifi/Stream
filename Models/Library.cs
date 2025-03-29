using System.ComponentModel.DataAnnotations;

namespace Stream.Models
{
    public class Library
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User? User { get; set; }

        [Required]
        public int GameId { get; set; }

        public Game? Game { get; set; }

        [Required]
        [StringLength(100)]
        public string? Status { get; set; }
    }
}