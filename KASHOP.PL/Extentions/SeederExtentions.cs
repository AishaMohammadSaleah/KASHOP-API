using KASHOP.PL.Utils;

namespace KASHOP.PL.Extentions
{
    public  static class SeederExtentions
    {
        public static async Task  AddSeedDataAsync(this WebApplication app) {

            using (var scope = app.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach (var seeder in seeders)
                {
                    await seeder.DataSeed();
                }



            }

        }
    }
}
