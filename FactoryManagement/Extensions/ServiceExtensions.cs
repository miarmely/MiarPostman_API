using Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Concrete;
using Services.Contracts;
using Services.Concretes;
using FactoryManagement.Responses.Contracts;
using FactoryManagement.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Sql.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"))
            );


        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();


        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();


        public static void ConfigureResponseBase(this IServiceCollection services) =>
            services.AddScoped<IResponseBase, ResponseBase>();


        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy(
                    name: "miarmely",
                    policy => policy
                        //.WithOrigins("http://127.0.0.1:5500", "http://localhost:5282")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()                    
                        )
            );
        }

    }
}