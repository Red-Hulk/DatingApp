using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    //Extension method moet static zijn
    public static class ApplicationServiceExtensions
    {
        //Parameter moet naar zichzelf refereren
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //Add service token, scoped is the lifetime of a httprequest, gebruik interface als je gaat mokken omdat je de klassen dan niet hoeft te implemteren.
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options => 
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}