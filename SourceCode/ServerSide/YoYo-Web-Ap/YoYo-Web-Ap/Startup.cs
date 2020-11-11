using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessRatingBeepRepository.Contracts;
using FitnessRatingBeepRepository.DataModel;
using FitnessRatingBeepServices.Contracts;
using FitnessRatingBeepServices.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace YoYo_Web_Ap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            //Services
            services.AddScoped<IFitnessRatingService,FitnessRatingService>();

            //Repository
            services.AddScoped<IFitnessRatingBeepRepository, FitnessRatingBeepRepository.Repository.FitnessRatingBeepRepository>();

            //Set mock athelete data
            var buildServiceProvider = services.BuildServiceProvider();
            var fitnessBeepRatingRepository = buildServiceProvider.GetRequiredService<IFitnessRatingBeepRepository>();
            fitnessBeepRatingRepository.InsertIntoAtheleteJsonData(GetDummyAtheleJsonData());

            services.AddControllersWithViews();
        }

       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        #region PRIVATE METHODS
        private List<AtheleteData> GetDummyAtheleJsonData()
        {
            var atheleteData = new List<AtheleteData>();
            for (int i = 1; i < 5; i++)
            {
                atheleteData.Add(new AtheleteData { Id = i, Name = $"user {i}"});
            }
            return atheleteData;
        }
        #endregion
    }
}
