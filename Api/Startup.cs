using Api.Middlewares;
using Business_Logic.Services;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Business_Logic.Helpers;
using Data_Transfer_Objects.Entities;
using Data_Transfer_Objects.Requests;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<SignInManager<User>>();

            services.AddAutoMapper(typeof(MapperProfile));

            services.AddCors();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => 
                {
                    var secret = Encoding.ASCII.GetBytes(Configuration["Jwt:Secret"]);
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });

            // TODO: Add facebook authentication
            
            // Services

            services.AddScoped<IdentityService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<UserService>();
            services.AddScoped<CourseService>();

            services.AddScoped<EmailService>();
            services.AddScoped<JobService>();
            
            // Helpers
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<RazorTemplateHelper>();
            
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration["ConnectionStrings:Default"], new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                })
            );

            services.AddHangfireServer();

            services.AddFluentValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            // Validations
            services.AddTransient<IValidator<UserDTO>, UserDTOValidation>();
            services.AddTransient<IValidator<CourseDTO>, CourseDTOValidation>();
            services.AddTransient<IValidator<CategoryDTO>, CategoryDTOValidation>();
            services.AddTransient<IValidator<CoursesRequest>, GetPageRequestValid>();
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidation>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidation>();
            
            services.AddControllersWithViews();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs)
        {
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Request: [{context.Request.Method}] {context.Request.Path}");
                await next.Invoke();
            });
        
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(options => options
               .WithOrigins(new[] { "http://localhost:3000", "https://localhost:3000" })
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
            );

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMiddleware<ErrorsHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}