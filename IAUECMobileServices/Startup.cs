using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Utility;

namespace IAUECMobileServices
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
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
       


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<BaseServiceConfig>(Configuration.GetSection("BaseServiceConfig"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "سرویس های اپلیکیشن موبایلی دانشگاه آزاد واحد الکترونیکی",
                    Description = "این سرویس به جهت ارتباط بین اپلیکیشن موبایلی و سامانه های دیگر طراحی گردیده است \r\n" +
                                  ":دارای مقادیر (ErrorId) خروجی\r\n" +
                                  " صفر:بدون خطا\r\n" +
                                  "یک: پسورد اشتباه\r\n" +
                                  "دو:ناموجود\r\n" +
                                  "سه:کد تایید اشتباه\r\n"+
                                  "چهار: درخواست بد"
                    ,
                    Version = "v1",
                    License = new License()
                    {
                        Name = " تیم طراحی و توسعه نرم افزار",
                        Url = "http://iauec.ac.ir"
                    },
                    Contact = new Contact
                    {

                        Name = "کارشناس فنی",
                        Email = "h_alizadeh@iauec.ac.ir",
                    }

                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "(v1)سرویس های اپلیکیشن موبایلی دانشگاه آزاد واحد الکترونیکی");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "دانشگاه آزاد واحد الکترونیکی";
                c.HeadContent = "<style>div.information-container{text-align: right !important;}div.opblock-summary-description{text-align: right !important;}</style>";
            });

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{

            //    c.SwaggerEndpoint("/swagger/v1/swagger.json",
            //        "(v1)سرویس های اپلیکیشن موبایلی دانشگاه آزاد واحد الکترونیکی");
            //    c.RoutePrefix = string.Empty;
            //    c.DocumentTitle = "دانشگاه آزاد واحد الکترونیکی";
            //    //c.HeadContent = "<style></style><script></script>";

            //});
            app.UseMvc();
        }
    }
}
