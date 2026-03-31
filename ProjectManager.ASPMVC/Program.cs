using Microsoft.Data.SqlClient;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.Common.Repositories;

namespace ProjectManager.ASPMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string
            string connectionString = builder.Configuration.GetConnectionString("ProjectManager.Database")!;


           
            builder.Services.AddControllersWithViews();

            //  Session
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();
            builder.Services.AddScoped<UserSessionManager>();

            // SQL Connection
            builder.Services.AddScoped<SqlConnection>(sp => new SqlConnection(connectionString));

            // DAL
            builder.Services.AddScoped<IEmployeeRepository<DAL.Entities.Employee>, DAL.Services.EmployeeService>();
            builder.Services.AddScoped<IProjectRepository<DAL.Entities.Project>, DAL.Services.ProjectService>();
            builder.Services.AddScoped<IPostRepository<DAL.Entities.Post>, DAL.Services.PostService>();
            builder.Services.AddScoped<IUserRepository<DAL.Entities.User>, DAL.Services.UserService>();
            builder.Services.AddScoped<ITakeRepository<DAL.Entities.TakePart>, DAL.Services.TakePartService>();

            // BLL
            builder.Services.AddScoped<IEmployeeRepository<BLL.Entities.Employee>, BLL.Services.EmployeeService>();
            builder.Services.AddScoped<IProjectRepository<BLL.Entities.Project>, BLL.Services.ProjectService>();
            builder.Services.AddScoped<IPostRepository<BLL.Entities.Post>, BLL.Services.PostService>();
            builder.Services.AddScoped<IUserRepository<BLL.Entities.User>, BLL.Services.UserService>();
            builder.Services.AddScoped<ITakeRepository<BLL.Entities.TakePart>, BLL.Services.TakePartService>();

            builder.Services.AddScoped<BLL.Services.UserService>();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession(); // IMPORTANT

            app.UseAuthorization();

            // Routing MVC (IMPORTANT)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            app.Run();
        }
    }
}