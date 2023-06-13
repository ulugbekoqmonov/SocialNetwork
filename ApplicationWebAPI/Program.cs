using Application;
using WebUI.GlobalExeptionHandler;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.TelegramBot;
using System.Text;
using WebUI.Middlewares;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace WebUI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.TelegramBot(
                token: builder.Configuration["TelegramBot:TelegramApiKey"],
                chatId: builder.Configuration["TelegramBot:TelegramChatId"],
                restrictedToMinimumLevel: LogEventLevel.Warning)
            .CreateLogger();
        try
        {
            Log.Information("Application is started");
            Build(builder);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, ex.Message);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void Build(WebApplicationBuilder builder)
    {
        #region Fixed Window
        //builder.Services.AddRateLimiter(options =>
        //{
        //    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        //    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        //        RateLimitPartition.GetFixedWindowLimiter(
        //            partitionKey: httpContext.Request.Headers.Host.ToString(),
        //            factory: partition => new FixedWindowRateLimiterOptions
        //            {
        //                AutoReplenishment = true,
        //                PermitLimit = 5,
        //                QueueLimit = 0,
        //                Window = TimeSpan.FromMinutes(12)
        //            }));
        //});
        #endregion
        #region Sliding Window
        //builder.Services.AddRateLimiter(rateLimiterOptions =>
        //{
        //    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        //    rateLimiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        //    RateLimitPartition.GetSlidingWindowLimiter(
        //        partitionKey: httpContext.Request.Headers.Host.ToString(),
        //        factory: partition => new SlidingWindowRateLimiterOptions
        //        {
        //            PermitLimit = 6,
        //            Window = TimeSpan.FromSeconds(20),
        //            SegmentsPerWindow = 2,
        //            QueueLimit = 0,
        //            AutoReplenishment = true,
        //        }));
        //});
        #endregion
        #region Cuncurrency
        //builder.Services.AddRateLimiter(rateLimiterOptions =>
        //{
        //    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        //    rateLimiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        //    RateLimitPartition.GetConcurrencyLimiter(
        //        partitionKey: httpContext.Request.Headers.Host.ToString(),
        //        factory: partition => new ConcurrencyLimiterOptions
        //        {
        //            PermitLimit = 6,
        //            QueueProcessingOrder=QueueProcessingOrder.OldestFirst,
        //            QueueLimit = 0,
        //        }));
        //});
        #endregion
        #region Token Bucket
        //builder.Services.AddRateLimiter(rateLimiterOptions =>
        //{
        //    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        //    rateLimiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        //    RateLimitPartition.GetTokenBucketLimiter(
        //        partitionKey: httpContext.Request.Headers.Host.ToString(),
        //        factory: partition => new TokenBucketRateLimiterOptions
        //        {
        //            TokenLimit = 4,
        //            ReplenishmentPeriod = TimeSpan.FromSeconds(10),
        //            TokensPerPeriod = 2,
        //            QueueLimit = 0,
        //            AutoReplenishment = true,
        //        }));
        //});
        #endregion
        builder.Host.UseSerilog();
        builder.Services.AddControllers();
        IConfiguration configuration = builder.Configuration;
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(configuration);
        builder.Services.AddResponseCaching();
        builder.Services.AddOutputCache();
        builder.Services.AddMemoryCache();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Authorization", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {{
                new OpenApiSecurityScheme()
                {
                    Reference=new OpenApiReference()
                    {
                        Id="Bearer",
                        Type=ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            } });
        });
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        var app = builder.Build();
        app.UseRateLimiter();
        app.UseSerilogRequestLogging();        
        app.UseHttpLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DisplayRequestDuration();
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseResponseCaching();
        app.UseOutputCache();

        app.UseGlobalExeptionHandler();
        app.UseETagger();
        app.MapControllers();

        app.Run();
    }
}