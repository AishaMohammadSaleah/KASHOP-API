using KASHOP.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.PL.Extentions
{
    public static class DatabaseExtentions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection Services,IConfiguration  Configuration) {
            Services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            return Services;


        }
    }
}
