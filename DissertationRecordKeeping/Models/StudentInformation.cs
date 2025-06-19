
using System.ComponentModel.DataAnnotations;

namespace DissertationRecordKeeping.Models
{
    public class StudentInformation
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [Required, StringLength(100)]
        public string School { get; set; }
        [Required, StringLength(50)]
        public string Department { get; set; }
        [Required, StringLength(50)]
        public string Program { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public int MatriculationNumber { get; set; }
        [Required, StringLength(50)]
        public string Supervisor { get; set; }
        [Required, StringLength(50)]
        public string DocumentType { get; set; }
        [Required, StringLength(50)]
        public string DocumentTitle { get; set; }
        [Required, StringLength(50)]
        public string Level { get; set; }
        [Required, StringLength(50)]
        public string Role { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
