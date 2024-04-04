using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotNetDiscordBot;

public class Bot : IBot
{
	ServiceProvider services;
	IConfiguration configuration;

	string key;
	string serverID_asString;
	ulong serverID;

    public Bot(ServiceProvider _services)
    {
		services = _services;
		configuration = services.GetRequiredService<IConfiguration>();

		key = configuration["DiscordBotKey"] ?? throw new Exception("--> DiscordBotKey <-- is missing in appsettings.json");
		serverID_asString = configuration["serverID"] ?? throw new Exception("--> ServerID <-- is missing in appsettings.json");
		serverID = Convert.ToUInt64(serverID_asString);
	}

    public async Task StartAsync()
	{
		var client = new DiscordClient(new DiscordConfiguration()
		{
			Token = key,
			TokenType = TokenType.Bot,
			Intents = DiscordIntents.AllUnprivileged
		});

		client.Ready += ClientReadyAsync;

		var slash = client.UseSlashCommands();
		slash.RegisterCommands<SlashCommands>(serverID);

		await client.ConnectAsync();
		await Task.Delay(-1);
	}

	private async Task ClientReadyAsync(DiscordClient sender, ReadyEventArgs args)
	{
		await Console.Out.WriteLineAsync("Bot is now running! Ready to go!");
	}
}