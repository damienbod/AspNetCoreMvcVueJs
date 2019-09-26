using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreMvcAngular.Repositories.Things;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreMvcAngular
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
            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = "https://localhost:44348";
                options.RequireHttpsMetadata = true;
                options.ClientId = "angularmvcmixedclient";
                options.ClientSecret = "thingsscopeSecret";
                options.ResponseType = "code id_token";
                options.Scope.Add("thingsscope");
                options.Scope.Add("profile");
                options.Prompt = "login"; // select_account login consent
                options.SaveTokens = true;
            });

            // TODO add policies 
            services.AddAuthorization();

            services.AddSingleton<IThingsRepository, ThingsRepository>();
            services.AddAntiforgery(options =>
            {
                options.Cookie.HttpOnly = false;
                options.HeaderName = "X-XSRF-TOKEN";
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
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

            //Registered before static files to always set header
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseCsp(opts => opts
                .BlockAllMixedContent()
                .ScriptSources(s => s.Self()).ScriptSources(s => s.UnsafeEval())
                .StyleSources(s => s.UnsafeInline())
            );

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var angularRoutes = new[] {
                 "/default",
                 "/about"
             };

            app.UseDefaultFiles();
            app.UseStaticFiles();

            //Registered after static files, to set headers for dynamic content.
            app.UseXfo(xfo => xfo.Deny());
            app.UseRedirectValidation(t => t.AllowSameHostRedirectsToHttps(44348)); 
            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                string path = context.Request.Path.Value;
                if (path != null && !path.ToLower().Contains("/api"))
                {
                    // XSRF-TOKEN used by angular in the $http if provided
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false });
                }

                if (context.Request.Path.HasValue && null != angularRoutes.FirstOrDefault(
                    (ar) => context.Request.Path.Value.StartsWith(ar, StringComparison.OrdinalIgnoreCase)))
                {
                    context.Request.Path = new PathString("/");
                }

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

