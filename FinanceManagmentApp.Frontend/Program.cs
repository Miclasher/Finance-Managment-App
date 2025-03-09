using FinanceManagmentApp.Frontend.Components;
using FinanceManagmentApp.Frontend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;

namespace FinanceManagmentApp.Frontend
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient("FinanceManagmentAppAPI",
                client => client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = builder.Configuration["Jwt:Audience"];
                    options.Authority = builder.Configuration["ApiBaseUrl"];
                });

            builder.Services.AddSingleton<CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());

            builder.Services.AddScoped<AuthService>();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
