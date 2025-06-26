using System.ComponentModel.DataAnnotations;

namespace DissertationRecordKeeping.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string School { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
