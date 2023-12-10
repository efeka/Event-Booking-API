using EventBookingAPI.Controllers;
using EventBookingAPI.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Dependency injection for services
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IEventService, EventService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        // Dependency injection for logger
        builder.Services.AddLogging(configure => configure.AddConsole());
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<UserController>();
        builder.Services.AddScoped<EventService>();
        builder.Services.AddScoped<EventController>();
        builder.Services.AddScoped<RoleService>();
        builder.Services.AddScoped<RoleController>();

        // Add logging services with a default configuration
        builder.Services.AddLogging();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configure CORS policies
        builder.Services.AddCors((options) =>
        {
            options.AddPolicy("DevCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins(
                    "http://localhost:4200",
                    "http://localhost:3000",
                    "http://localhost:8000"
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
            options.AddPolicy("ProdCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("https://myProductionSite.com")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseCors("DevCors");
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseCors("ProdCors");
            app.UseHttpsRedirection();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
