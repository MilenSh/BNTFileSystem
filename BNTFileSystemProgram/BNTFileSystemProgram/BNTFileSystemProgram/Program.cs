namespace BNTFileSystemProgram;
using BussinessLayer;
using DataLayer;


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.AddScoped<IDb<Author, string>, AuthorContext>();
            builder.Services.AddScoped<IDb<Format, string>, FormatContext>();
            builder.Services.AddScoped<IDb<Genre, string>, GenreContext>();
            builder.Services.AddScoped<IDb<Tag, string>, TagContext>();
            builder.Services.AddScoped<IDb<Video, string>, VideoContext>();

        builder.Services.AddControllersWithViews();
            
            









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
