using System.ComponentModel.DataAnnotations;

namespace DissertationRecordKeeping.Models
{
    public class Admin
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [Required, StringLength(50)]
        public string Username { get; set; }
        [Required, StringLength(50)]
        public string School { get; set; }
        [Required, StringLength(50)]
        public string ContactNumber { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Role { get; set; }
        [Required]
        public string Password { get; set; }
        // Assign role if possible
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
