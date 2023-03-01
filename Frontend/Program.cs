
using CUGOJ.Frontend.Middlewares;
using CUGOJ.Tools.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using CUGOJ.Tools;

namespace CUGOJ.Frontend
{
    public static class Program
    {
        public static void InitBuilder(WebApplicationBuilder builder)
        {
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            if(Config.Debug)
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("debug",policy =>
                        {
                            policy.WithOrigins("http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        });
                });
            }
            
            builder.Services.AddControllers().
                AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<LoginMiddleware>();
        }
        public static void InitWebApplication(WebApplication app)
        {  
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            if(Config.Debug)
            {
                app.UseCors("debug");
            }

            app.MapControllers();
            app.UseMiddleware<LoginMiddleware>();
            //app.UseMiddleware<>
            app.MapGet("api/Ping", () =>
            {
                var user =  ContextTools.GetUserId();
                if (user == null)
                    return "游客,欢迎来到CUGOJ";
                else
                    return $"{user}号用户,欢迎来到CUGOJ";
            });
        }
    }
}
