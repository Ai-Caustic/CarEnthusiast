using CarEnthusiast.Data;
using Microsoft.EntityFrameworkCore;
namespace CarEnthusiast
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(@"DefaultConnection")));

            services.AddControllersWithViews();
        }
    }
}
