using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Linq;

namespace WebApplication6
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddAuthentication(o =>
                {
                    //o.DefaultAuthenticateScheme = "Cookie";
                    o.DefaultSignInScheme = "Cookie";
                })
                .AddCookie("Cookie")
                .AddFacebook("Facebook", o =>
                {
                    o.ClientId = "729947880734922";
                    o.ClientSecret = "42d060e65d8962e15a3ddd480c230ce5";
                })
                .AddGoogle("Google", o =>
                {
                    o.ClientId = "762574578671-0fieldv302nufou2afqfu4tgt0nopdtq.apps.googleusercontent.com";
                    o.ClientSecret = "yzXo3JOV3PsrfRoXqsso-G0K";
                })
                .AddOAuth("Github", o =>
                {
                    o.ClientId = "f65ec9d979c76c9dba76";
                    o.ClientSecret = "25dc28d5af66b21438625f4f63f084482b96a7c0";

                    o.CallbackPath = new PathString("/github");

                    o.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                    o.TokenEndpoint = "https://github.com/login/oauth/access_token";
                    o.UserInformationEndpoint = "https://api.github.com/user";

                    o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    o.ClaimActions.MapJsonKey("urn:github:login", "login");
                    o.ClaimActions.MapJsonKey("urn:github:url", "html_url");
                    o.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

                    o.Events = new OAuthEvents
                    {
                        OnCreatingTicket = async context =>
                        {
                            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                            var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();

                            var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                            context.RunClaimActions(user);
                        }
                    };
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Scripts/dist")),
                    RequestPath = "/Scripts"
                });
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
