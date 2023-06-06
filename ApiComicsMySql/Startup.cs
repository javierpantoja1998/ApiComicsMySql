using ApiComicsMySql.Data;
using ApiComicsMySql.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiComicsMySql;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container

    public void ConfigureServices(IServiceCollection services)

    {

        string connectionString =

            this.Configuration.GetConnectionString("MySql");

        services.AddTransient<RepositoryComics>();

        services.AddDbContext<ComicsContext>

            (options => options.UseMySql(connectionString,

            ServerVersion.AutoDetect(connectionString)));

        services.AddCors(options =>

        {

            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());

        });

        services.AddControllers();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}