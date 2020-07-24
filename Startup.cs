using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginUsingJwtV2.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LoginUsingJwtV2
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddDbContext<LoginDbContext>(options =>
             options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));


            //get jwt setting from appsetting.json
            JwtSettings settings;
            settings = GetJwtSettings();

            //create singleton of jwtsettings
            services.AddSingleton<JwtSettings>(settings);
            services.AddTransient<LoginDbContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // regiset jwt as authentication service
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";

            }).AddJwtBearer("JwtBearer", JwtBearerOptions => {
                JwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
                    ValidIssuer = settings.Issuer,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = settings.Audiance,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(settings.MinutesToExpiration)
                };
            });
            //The claim type and value are case sensitive
            services.AddAuthorization(cfg => {
                cfg.AddPolicy("CanAccessProducts", P =>
                P.RequireClaim("CanAccessProducts","true"));
                });

            services.AddCors();
            services.AddRazorPages();
         
        }

      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

          
           
       

            app.UseRouting();

            app.UseAuthorization();
            
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });




        }



        public JwtSettings GetJwtSettings()
        {
            JwtSettings settings = new JwtSettings();

            settings.Key = Configuration["JwtSettings:Key"];
            settings.Issuer= Configuration["JwtSettings:issuer"];
            settings.Audiance= Configuration["JwtSettings:audiance"];
            settings.MinutesToExpiration =Convert.ToInt32(Configuration["JwtSettings:minutesToExpiration"]);
            return settings;
        }



    }
}
