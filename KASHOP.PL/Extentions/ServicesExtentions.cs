namespace KASHOP.PL.Extentions
{
    public static class ServicesExtentions
    {
        public static IServiceCollection AddServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddControllers();
            Services.AddOpenApi();
            Services.AddDatabaseServices( Configuration);
            Services.AddLocalizationServices();
            Services.AddApplicationServices();
            Services.AddIdentityServices();
            Services.AddJwtAuthServices(Configuration);
           
        
            return Services;

        }
    }
}


