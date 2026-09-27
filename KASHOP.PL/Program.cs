using KASHOP.PL.Extentions;
using Microsoft.Extensions.Options;


namespace KASHOP.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices(builder.Configuration);
            builder.Services.AddAuthorization();
            var app = builder.Build();
            // 2. Configure HTTP Request Pipeline
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // Routing MUST come before Authentication & Authorization
            app.UseRouting();

            app.UseAuthentication(); // ÷—Ê—Ì · ›⁄Ì· JWT
            app.UseAuthorization();

            app.MapControllers();

            // 3. Seed Data
            await app.AddSeedDataAsync();

            // 4. Run Application
            app.Run();
        }
    } }
