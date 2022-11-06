global using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Movies.API.Domain;
using Movies.API.Helper;
using Movies.API.Services;

namespace Movies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddFastEndpoints();
            builder.Services.AddSwaggerDoc();

            //builder.Services.AddCors(o => o.AddPolicy(name: "Default",
            //         policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
            //var httpClient = new HttpClient();
            //var response = httpClient.Send(new HttpRequestMessage { RequestUri = new Uri("http://syncendpoint.local") });
            //var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter()));
            builder.Services.AddDbContext<MoviesDbContext>(x => x.UseSqlServer("Default"));
            builder.Services.AddScoped<IMoviesService, MoviesService>();

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
            app.Run("http://localhost:5050");
        }
    }
}