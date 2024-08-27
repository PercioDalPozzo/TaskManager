using Domain.Commands.Taskconclude;
using Domain.Commands.TaskConclude;
using Domain.Commands.TaskCreate;
using Domain.Commands.TaskDelete;
using Domain.Commands.TaskQuery;
using Domain.Commands.UserRegister;
using Domain.Interfaces;
using Domain.Services;
using Repository;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Informações da API",
                Version = "v1",
                Description = "Task Manager",
            });
        });


        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        builder.Services.AddScoped<ICommandHandler<UserRegisterCommand>, UserRegisterCommandHandler>();
        builder.Services.AddScoped<ICommandResultHandler<TaskCreateCommand, Guid>, TaskCreateCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<TaskDeleteCommand>, TaskDeleteCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<TaskConcludeCommand>, TaskConcludeCommandHandler>();
        builder.Services.AddScoped<IQueryHandler<TaskQuery, TaskQueryResponse>, TaskQueryHandler>();


        


        //builder.Services.AddControllers()
        //   .AddJsonOptions(options =>
        //   {
        //       options.JsonSerializerOptions.PropertyNamingPolicy = null;
        //   });

        //builder.Services.AddHttpClient();

        builder.Services.AddControllers();
        
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.MapControllers();

        app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager");
            });
        }

        app.Run();
    }
}