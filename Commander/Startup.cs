using System;
using AutoMapper;
using Commander.DataBase.Context;
using Commander.Repositories;
using Commander.Repositories.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace Commander
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
      services.AddControllers().AddNewtonsoftJson(s =>
        s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
      // DbContext
      services
        .AddEntityFrameworkNpgsql()
        .AddDbContext<CommanderDbContext>(options => options.UseNpgsql(Configuration["Commander.PostgreSQL"]));
      // Repositories
      services.AddScoped<ICommanderRepository, PostgreSQLCommanderRepository>();
      // Auto Mapper
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
