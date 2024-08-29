using Api;
using Domain.Commands.NotificationQuery;
using Domain.Commands.NotificationRead;
using Domain.Commands.TaskConclude;
using Domain.Commands.TaskCreate;
using Domain.Commands.TaskDelete;
using Domain.Commands.TaskQuery;
using Domain.Commands.UserRegister;
using Domain.Interfaces;
using Domain.Job;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Repository;
using Repository.Context;
using System.Text;

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

            // Definição do esquema de segurança para JWT
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT aqui SEM o 'Bearer' {seu token}"
            });

            // Adiciona o requisito de segurança ao Swagger
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });


        //Repository
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        //Handlers and queries
        builder.Services.AddScoped<ICommandResultHandler<UserRegisterCommand, Guid>, UserRegisterCommandHandler>();
        builder.Services.AddScoped<ICommandResultHandler<TaskCreateCommand, Guid>, TaskCreateCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<TaskDeleteCommand>, TaskDeleteCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<TaskConcludeCommand>, TaskConcludeCommandHandler>();
        builder.Services.AddScoped<IQueryHandler<TaskQuery, TaskQueryResponse>, TaskQueryHandler>();

        builder.Services.AddScoped<ICommandHandler<NotificationReadCommand>, NotificationReadCommandHandler>();
        builder.Services.AddScoped<IQueryHandler<NotificationQuery, NotificationQueryResponse>, NotificationQueryHandler>();


        // InMemory DB
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

        // Quartz
        var minutes = builder.Configuration.GetValue<int>("IntervalMinutesToNotificationJob");
        builder.Services.AddQuartz(q =>
        {
            var jobKey = new JobKey("NotificationJob");
            q.AddJob<NotificationJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("NotificationJob-trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(minutes)
                    .RepeatForever()));
        });
        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);



        // Configuração JWT
        var key = Encoding.ASCII.GetBytes(JwtUtil.SecurityKey);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
            };
        });



        builder.Services.AddControllers();

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.MapControllers();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

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