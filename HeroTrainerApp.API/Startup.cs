﻿using System.Net;
using System.Text;
using AutoMapper;
using HeroTrainerApp.API.Data;
using HeroTrainerApp.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HeroTrainerApp.API
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

            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper(typeof(AuthRepository).Assembly);
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });
            services.AddCors();


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
                // app.UseHsts();
                app.UseExceptionHandler(builder =>
               {
                   builder.Run(async context =>
                   {

                       context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                       var error = context.Features.Get<IExceptionHandlerFeature>();
                       if (error != null)
                       {
                           context.Response.AddApplicationError(error.Error.Message);
                           await context.Response.WriteAsync(error.Error.Message);
                       }
                   }
                   );
               });
            }

            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}
