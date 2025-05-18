using DissertationRecordKeeping.Models;
using Microsoft.EntityFrameworkCore;

namespace DissertationRecordKeeping.Data
{
    public class RecordContext : DbContext
    {
        public RecordContext(DbContextOptions<RecordContext> options) : base(options) { }
        public DbSet<StudentInformation> StudentInformations { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}