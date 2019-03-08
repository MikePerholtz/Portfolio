using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Portfolio.Models
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {
            public DbSet<Contact> Contacts { get; set; }
        }
    }

    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
    
