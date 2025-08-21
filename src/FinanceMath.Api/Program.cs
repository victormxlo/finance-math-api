using FinanceMath.Application.Users.Commands.RegisterUser;
using FinanceMath.Application.Users.Queries.GetUserById;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserHandler).Assembly));

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(GetUserByIdHandler).Assembly));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.Run();
    }
}