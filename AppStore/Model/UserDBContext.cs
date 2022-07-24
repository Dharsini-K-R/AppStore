using Microsoft.EntityFrameworkCore;

namespace AppStore.Model
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {

        }

        //Create Table here
        public DbSet<Registration> Users { get; set; }
    }
}
