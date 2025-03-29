using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stream.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nazwa użytkownika")]
        public string? Username { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Hasło")]
        public string? Password { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Library> Libraries { get; set; } = new List<Library>();
    }

}
