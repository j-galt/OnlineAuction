using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.Core.Interfaces;
using OnlineAuction.Core.Services;
using OnlineAuction.Infrastructure;
using OnlineAuction.Infrastructure.Identity;
using OnlineAuction.Infrastructure.Repositories;

namespace OnlineAuction.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ILotRepository, LotRepository>();
            services.AddScoped<ILotStateRepository, LotStateRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILotService, LotService>();
            services.AddScoped<IBidService, BidService>();

            services.AddDbContext<OnlineAuctionDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<OnlineAuctionDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Lot/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{lotPage:int}",
                    defaults: new { controller = "Lot", action = "List" });

                routes.MapRoute(
                    name: null,
                    template: "Page{lotPage:int}",
                    defaults: new { controller = "Lot", action = "List", lotPage = 1 });

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Lot", action = "List", lotPage = 1 });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}