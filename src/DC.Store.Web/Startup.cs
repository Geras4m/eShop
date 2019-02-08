using AutoMapper;
using DC.Store.Core.DI;
using DC.Store.Core.Model;
using DC.Store.Data.DI;
using DC.Store.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DC.Store.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());

            services.AddSession();

            services.AddDbContext<DcStoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainConnection")), ServiceLifetime.Transient);

            services.AddCoreServices();
            services.AddRepositoryServices();

            Mapper.Initialize(cfg =>
            {
                #region Map from Entity to Dto
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Product, ProductDto>();
                #endregion

                #region Map from Dto Entity
                cfg.CreateMap<CategoryDto, Category>();
                cfg.CreateMap<ProductDto, Product>();
                #endregion

                #region Map from Dto to ResponseModel
                cfg.CreateMap<CategoryDto, Models.CategoryViewModel>();
                cfg.CreateMap<ProductDto, Models.ProductViewModel>();
                #endregion
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
