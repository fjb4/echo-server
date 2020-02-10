using System;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EchoServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                    await context.Response.WriteAsync(BuildEchoText(configuration)));
            });
        }

        private static string BuildEchoText(IConfiguration configuration)
        {
            const string defaultText = "Hello World at {{now}}!";

            var echoText = configuration.GetValue<string>("text");

            if (string.IsNullOrWhiteSpace(echoText))
            {
                echoText = defaultText;
            }

            var nowStr = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            return echoText.Replace("{{now}}", nowStr, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}