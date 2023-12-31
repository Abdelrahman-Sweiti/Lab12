using Lab12.Data;
using Lab12.Models;
using Lab12.Models.DTO;
using Lab12.Models.Interfaces;
using Lab12.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Lab12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();


            builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<HotelContext>
            (opions => opions.UseSqlServer(connString));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                }).AddEntityFrameworkStores<HotelContext> ();
            builder.Services.AddScoped<JwtTokenService>();




            builder.Services.AddTransient<IUser, UserService>();
            builder.Services.AddTransient<IAmenity, AmenityService>();
            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IHotel, HotelService>();
            builder.Services.AddTransient<IHotelRoom, HotelRoomService>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Tell the authenticaion scheme "how/where" to validate the token + secret
                options.TokenValidationParameters = JwtTokenService.GetValidationPerameters(builder.Configuration);
            });

            builder.Services.AddAuthorization(options =>
            {
                // Add "Name of Policy", and the Lambda returns a definition
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
                options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
                options.AddPolicy("deposit", policy => policy.RequireClaim("permissions", "deposit"));
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()

                {
                    Title = "Lab12",
                    Version = "v1",


                }


                    );
            
            
            
            });






            var app = builder.Build();


            app.UseSwagger(options => {

                options.RouteTemplate = "/api/{documentName}/swagger.json";
            
            
            });


            app.UseSwaggerUI(options => {

                options.SwaggerEndpoint("/api/v1/swagger.json","Lab12");
                options.RoutePrefix = "docs";
            
            
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}