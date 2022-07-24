using Microsoft.EntityFrameworkCore;

namespace AppStore.Model
{
    public class AppModelDBContext : DbContext
    {
        public AppModelDBContext(DbContextOptions<AppModelDBContext> options) : base(options)
        {

        }

        //Create Table here
        public DbSet<AppModel> AppList { get; set; }
    }
}
