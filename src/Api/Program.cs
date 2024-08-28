using Domain.Commands.NotificationQuery;
using Domain.Commands.Taskconclude;
using Domain.Commands.TaskConclude;
using Domain.Commands.TaskCreate;
using Domain.Commands.TaskDelete;
using Domain.Commands.TaskQuery;
using Domain.Commands.UserRegister;
using Domain.Interfaces;
using Domain.Job;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Repository;
using Repository.Context;
using Microsoft.Extensions.DependencyInjection;

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


        //Repository
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        //Handlers and queries
        builder.Services.AddScoped<ICommandResultHandler<UserRegisterCommand,Guid>, UserRegisterCommandHandler>();
        builder.Services.AddScoped<ICommandResultHandler<TaskCreateCommand, Guid>, TaskCreateCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<TaskDeleteCommand>, TaskDeleteCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<TaskConcludeCommand>, TaskConcludeCommandHandler>();
        builder.Services.AddScoped<IQueryHandler<TaskQuery, TaskQueryResponse>, TaskQueryHandler>();

        builder.Services.AddScoped<ICommandHandler<NotificationReadCommand>, NotificationReadCommandHandler>();
        builder.Services.AddScoped<IQueryHandler<NotificationQuery, NotificationQueryResponse>, NotificationQueryHandler>();


        // InMemory DB
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("InMemoryDb"));


        //builder.Services.AddControllers()
        //   .AddJsonOptions(options =>
        //   {
        //       options.JsonSerializerOptions.PropertyNamingPolicy = null;
        //   });

        //builder.Services.AddHttpClient();


        // Quartz
        var minutes = builder.Configuration.GetValue<int>("IntervalMinutesToNotificationJob");

        builder.Services.AddQuartz(q =>
        {
            var jobKey = new JobKey("NotificationJob");
            q.AddJob<NotificationJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey) // Adiciona o trigger ao job
                .WithIdentity("NotificationJob-trigger") // Identifica o trigger
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(minutes) // Executa a cada 60 minutos
                    .RepeatForever())); // Repetir para sempre
        });

        // Adiciona o Quartz.NET como um serviço  hospedado
        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


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