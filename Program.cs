
using CoffeeShopCore.Repository;
using CoffeeShopCore.Services;
using CoffeeShopData.DbContext;
using CoffeeShopData.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CoffeeShopAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext Service to the container.
            builder.Services.AddDbContext<CoffeeShopDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            //Adding identity service to the container
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<CoffeeShopDbContext>()
                .AddDefaultTokenProviders();


            //registering the service
            builder.Services.AddScoped<IOrderRepository,OrderRepository>();
            builder.Services.AddScoped<ISignUpRepository, SignUpRepository>();




            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}