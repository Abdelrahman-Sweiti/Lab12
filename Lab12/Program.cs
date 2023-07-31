using Lab12.Data;
using Lab12.Models.Interfaces;
using Lab12.Models.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
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

            builder.Services.AddControllers();

            builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<HotelContext>
            (opions => opions.UseSqlServer(connString));

            builder.Services.AddTransient<IHotel, HotelService>();
            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IAmenity, AmenityService>();
            builder.Services.AddTransient<IHotelRoom, HotelRoomService>();


            var app = builder.Build();

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

            app.UseAuthorization();
            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}