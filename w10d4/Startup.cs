using System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using w10d4.Repositories;
using w10d4.Services;

namespace w10d4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCors(services);
            ConfigureAuth(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "w10d4", Version = "v1" });
            });
            services.AddScoped<IDbConnection>(x => CreateDbConnection());
            
            services.AddScoped<AccountsRepository>();
            services.AddScoped<AccountService>();

            services.AddTransient<FavoritesRepository>();
            services.AddTransient<FavoritesService>();

            services.AddTransient<IngredientsRepository>();
            services.AddTransient<IngredientsService>();

            services.AddTransient<StepsRepository>();
            services.AddTransient<StepsService>();

            services.AddTransient<RecipesRepository>();
            services.AddTransient<RecipesService>();
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsDevPolicy", builder =>
                {
                    builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(new string[]{
                        "http://localhost:8080", "http://localhost:8081"
                    });
                });
            });
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["AUTH0_DOMAIN"]}/";
                options.Audience = Configuration["AUTH0_AUDIENCE"];
            });

        }

        private IDbConnection CreateDbConnection()
        {
            string connectionString = Configuration["CONNECTION_STRING"];
            return new MySqlConnection(connectionString);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "w10d4 v1"));
                app.UseCors("CorsDevPolicy");
            }

            app.UseHttpsRedirection();
            
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
