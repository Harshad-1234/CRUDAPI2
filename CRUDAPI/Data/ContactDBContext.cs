using CRUDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Data
{
    public class ContactDBContext : DbContext
    {
        public ContactDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
