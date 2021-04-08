using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NetCoreLog4NetWebDemo
{
    public class Startup
    {
        public static ILoggerRepository Log4NetRepository { get; set; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log4NetRepository = LogManager.CreateRepository("NetCoreLog4NetWebDemo");
            XmlConfigurator.Configure(Log4NetRepository, new FileInfo("Config/Log4Net.config"));

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(); // Dick 2021-03-11 [ 添加应用程序会话状态所需的服务 ]
            services.AddControllersWithViews(); // Dick 2021-03-30 [ 标准MVC模式 ]
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Dick 2021-03-12 [ 本地环境异常抛出到页面 ]
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Dick 2021-03-12 [ 非本地环境异常，跳转异常页 ]
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();  // Dick 2021-03-11 [ 配置中间件提供静态文件 ]
            app.UseRouting(); // Dick 2021-03-11 [ 中间件，根据当前请求找到endpoint ]
            app.UseAuthentication();  // Dick 2021-03-11 [ 认证中间件，明确是你谁，确认是不是合法用户。常用的认证方式有用户名密码认证。 ]
            app.UseAuthorization();   // Dick 2021-03-11 [ 授权中间件 ]  
            app.UseSession();  // Dick 2021-03-11 [ Session中间件，使用前需要在ConfigureServices中注册AddSession ]  
            app.UseEndpoints(endpoints => // Dick 2021-03-11 [ 拿到上面找到的endpoint去执行请求的最终处理 ]
            {
                endpoints.MapDefaultControllerRoute();
                // Dick 2021-03-11 [ 此语句等同于上一句 ]
                //endpoints.MapControllerRoute(name:"default",pattern:"{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(name: "Areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}
