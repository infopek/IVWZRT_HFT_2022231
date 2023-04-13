using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using Microsoft.OpenApi.Models;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Repository;
using IVWZRT_HFT_2022231.Models;
using IVWZRT_HFT_2022231.Endpoint.Services;

namespace IVWZRT_HFT_2022231.Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ApexDbContext>();

            services.AddTransient<IRepository<Player>, PlayerRepository>();
            services.AddTransient<IRepository<Legend>, LegendRepository>();
            services.AddTransient<IRepository<Match>, MatchRepository>();
            services.AddTransient<IRepository<EndGameStat>, StatRepository>();

            services.AddTransient<IPlayerLogic, PlayerLogic>();
            services.AddTransient<ILegendLogic, LegendLogic>();
            services.AddTransient<IMatchLogic, MatchLogic>();
            services.AddTransient<IStatLogic, StatLogic>();

            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IVWZRT_HFT_2022231.Endpoint", Version = "v1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IVWZRT_HFT_2022231.Endpoint v1"));
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:41372"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }

        public IConfiguration Configuration { get; }
    }
}
