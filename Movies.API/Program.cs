using Common;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Movies.API.Domain;
using Movies.API.Helpers;
using Movies.API.Services;

namespace Movies.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddFastEndpoints();
            builder.Services.AddSwaggerDoc();
            builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter()));
            builder.Services.AddDbContext<MoviesDbContext>(x => x.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING")));
            builder.Services.AddScoped<IMoviesService, MoviesService>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.UseCors("Default");
            app.UseAuthorization();
            app.UseFastEndpoints();

            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3(s => s.ConfigureDefaults());
            }

            await using var scope = app.Services.CreateAsyncScope();
            using var db = scope.ServiceProvider.GetService<MoviesDbContext>();
            await DoWithRetryAsync(async () => { await db.Database.MigrateAsync(); }, TimeSpan.FromSeconds(2), 10);

            var syncNodeClient = new SyncNodeClient();
            await syncNodeClient.RegisterNode(new NodeInfoDto { IpAddress = Environment.GetEnvironmentVariable("CONTAINER_IP") });

            app.Run();
        }
        public static async Task DoWithRetryAsync(Func<Task> action, TimeSpan sleepPeriod, int tryCount = 3)
        {
            if (tryCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(tryCount));

            while (true)
            {
                try
                {
                    await action();
                    return; // success!
                }
                catch
                {
                    if (--tryCount == 0)
                        throw;
                    await Task.Delay(sleepPeriod);
                }
            }
        }

    }
}