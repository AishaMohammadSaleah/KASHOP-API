using KASHOP.BLL.Common;
using KASHOP.BLL.Services;
using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using KASHOP.PL.Extentions;
using KASHOP.PL.Utils;
using Microsoft.Extensions.Options;


namespace KASHOP.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddDatabaseServices(builder.Configuration);
            builder.Services.AddLocalizationServices();
            builder.Services.AddAuthorization();
            builder.Services.AddJwtAuthServices(builder.Configuration);
            builder.Services.AddIdentityServices();
            builder.Services.AddApplicationServices();
            var app = builder.Build();
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
 
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();


            using (var scope = app.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach (var seeder in seeders)
                {
                   await seeder.DataSeed();
                }



            }
            app.MapControllers();

            app.Run();
        }
    }
}
