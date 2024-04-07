using dotNetDiscordBot;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Reflection;

public class Program
{
	static async Task Main(string[] args)
	{
		//	Set up configuration for Serilog
		var builder = new ConfigurationBuilder();
		BuildConfig(builder);

		Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(builder.Build())
			.Enrich.FromLogContext()
			.WriteTo.Console()
			.CreateLogger();

		//	Set up the Host and add services
		var host = Host.CreateDefaultBuilder()
			.UseConsoleLifetime()
			.ConfigureServices((context, services) =>
			{
				services.AddHostedService<BotService>();
			})
			.UseSerilog()
			.Build();
		
		await host.RunAsync();

		await Log.CloseAndFlushAsync();

		//var bot = ActivatorUtilities.CreateInstance<Bot>(host.Services);
		//await bot.StartAsync();
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