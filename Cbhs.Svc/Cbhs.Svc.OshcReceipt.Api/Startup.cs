using System.Reflection;
using Cbhs.Svc.OshcReceipt.Application.Interfaces;
using Cbhs.Svc.OshcReceipt.Application.Interfaces.Infrastructure;
using Cbhs.Svc.OshcReceipt.Application.Queries.GetQuote;
using Cbhs.Svc.OshcReceipt.Infrastructure;
using Cbhs.Svc.OshcReceipt.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cbhs.Svc.OshcReceipt.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add MediatR
            services.AddMediatR(typeof(GetQuoteQueryHandler).GetTypeInfo().Assembly);

            // Add DBContext
            services.AddDbContext<IReceiptDbContext, ReceiptDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ParagonDatabase")));

            // Add framework services.
            services.AddHttpClient<IRebateService, RebateService>();
            services.AddHttpClient<IProductService, ProductService>();
            services.AddHttpClient<IDiscountService, DiscountService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}