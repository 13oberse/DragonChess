using DragonChessUI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading.Tasks;

namespace DragonChessUI;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
		{
			Args = args,
#if !DEBUG
            ContentRootPath = "/app"
#endif
		});

		builder.Services.Configure<ForwardedHeadersOptions>(options =>
		{
			options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			options.KnownProxies.Clear();
			options.KnownProxies.Add(IPAddress.Parse("172.16.6.1"));
		});

		// Add services to the container.
		builder.Services.AddRazorPages();
		builder.Services.AddServerSideBlazor();
		builder.Services.AddSingleton<WeatherForecastService>();
		builder.Services.AddPlayerManager();

		var app = builder.Build();
		app.UseForwardedHeaders();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();

		app.MapBlazorHub();
		app.MapFallbackToPage("/_Host");

		await app.RunAsync();
	}
}
