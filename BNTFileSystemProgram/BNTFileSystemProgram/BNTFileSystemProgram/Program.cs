namespace BNTFileSystemProgram;
using BussinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;

public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddScoped<AuthorContext, AuthorContext>();
            builder.Services.AddScoped<AuthorManager, AuthorManager>();

            builder.Services.AddScoped<FormatContext, FormatContext>();
            builder.Services.AddScoped<FormatManager, FormatManager>();

            builder.Services.AddScoped<GenreContext, GenreContext>();
            builder.Services.AddScoped<GenreManager, GenreManager>();

            builder.Services.AddScoped<TagContext, TagContext>();
            builder.Services.AddScoped<TagManager, TagManager>();

            builder.Services.AddScoped<VideoContext, VideoContext>();
            builder.Services.AddScoped<VideoManager, VideoManager>();

        builder.Services.AddScoped<IdentityContext>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddIdentity<BusinessLayer.User, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddRoles<IdentityRole>()
        .AddDefaultTokenProviders();
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;
        });








        var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
