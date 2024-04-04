using System;
using System.Threading.Tasks;
using dotNetDiscordBot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class Program
{
	static async Task Main(string[] args)
	{
		//	init and setting up config
		var config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", false, true)
			.Build();
		

		var serviceProvider = new ServiceCollection()
			.AddSingleton<IConfiguration> (config)
			.BuildServiceProvider();

		var bot = new Bot(serviceProvider);
		await bot.StartAsync();
	}
}