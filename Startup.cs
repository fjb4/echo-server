using System;
using System.Globalization;
using System.Linq;
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
                {
                    var text = BuildEchoText(configuration) + Environment.NewLine;

                    if (configuration["timestamp"] != null)
                    {
                        text += DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) + Environment.NewLine;
                    }

                    if (configuration["headers"] != null)
                    {
                        text += GetHeaderText(context.Request.Headers) + Environment.NewLine;
                    }

                    await context.Response.WriteAsync(text);
                });
            });
        }

        private static string BuildEchoText(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            const string defaultText = "Hello World!";

            var echoText = configuration.GetValue<string>("text");

            if (string.IsNullOrWhiteSpace(echoText))
            {
                echoText = defaultText;
            }

            return echoText;
        }

        private static string GetHeaderText(IHeaderDictionary headers)
        {
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            var headerText = string.Empty;

            foreach (var (key, value) in headers.OrderBy(h => h.Key))
            {
                headerText += $"{key} = {value}" + Environment.NewLine;
            }

            return headerText;
        }
    }
}