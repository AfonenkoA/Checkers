using Microsoft.EntityFrameworkCore;
using Checkers.Site.Data.Models;

namespace Checkers.Site
{
    internal class UserContex :DbContext
    {
        public UserContex(DbContextOptions<UserContex> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }
    }
}
