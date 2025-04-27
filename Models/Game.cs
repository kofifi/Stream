using System.ComponentModel.DataAnnotations;

namespace Stream.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Required]
        public Platform Platform { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}