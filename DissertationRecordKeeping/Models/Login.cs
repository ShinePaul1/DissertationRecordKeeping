using System.ComponentModel.DataAnnotations;

namespace DissertationRecordKeeping.Models
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string School { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
