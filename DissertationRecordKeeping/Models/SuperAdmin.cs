using Azure.Identity;
using System.ComponentModel.DataAnnotations;

namespace DissertationRecordKeeping.Models
{
    public class SuperAdmin
    {
        public int Id { get; set; }
        [Required, StringLength(10)]
        public required string UserName {get; set; }
        [Required, StringLength(10)]
        public string School {  get; set; }
        [Required, StringLength(20)]
        public required string Role { get; set; }
        [Required]
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
