using System;
using System.Threading.Tasks;
using dotNetDiscordBot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

public class Program
{
	static async Task Main(string[] args)
	{

		var builder = new ConfigurationBuilder();
		BuildConfig(builder);

		Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(builder.Build())
			.Enrich.FromLogContext()
			.WriteTo.Console()
			.CreateLogger();

		var host = Host.CreateDefaultBuilder()
			.ConfigureServices((context, services) =>
			{
				services.AddTransient<IBot, Bot>();
			})
			.UseSerilog()
			.Build();

		var bot = ActivatorUtilities.CreateInstance<Bot>(host.Services);
		await bot.StartAsync();
	}

	static void BuildConfig(IConfigurationBuilder builder)
	{
		builder.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT") ?? "Produktion"}.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables()
			.Build();
	}
}