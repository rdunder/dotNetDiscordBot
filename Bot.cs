using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DSharpPlus.SlashCommands.EventArgs;
using Serilog;

namespace dotNetDiscordBot;

public class Bot : IBot
{
	private readonly ILogger<Bot> _log;
	private readonly IConfiguration _config;

	string key;
	ulong serverID;

    public Bot(ILogger<Bot> log, IConfiguration config)
    {
		_config = config;
		_log = log;

		key = _config.GetValue<string>("DiscordBotKey") ?? throw new Exception("--> DiscordBotKey <-- is missing in appsettings.json");
		serverID = _config.GetValue<ulong>("ServerID");
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
		slash.SlashCommandExecuted += SlashCommandExecutedAsync;

		await client.ConnectAsync();
		await Task.Delay(-1);
	}

	private async Task SlashCommandExecutedAsync(SlashCommandsExtension sender, SlashCommandExecutedEventArgs args)
	{
		var ctx = args.Context;
		_log.LogInformation("{user} used the {command} command, in the channel: {channel}", ctx.Member.DisplayName, ctx.CommandName, ctx.Channel.Name);
	}

	private async Task ClientReadyAsync(DiscordClient sender, ReadyEventArgs args)
	{
		_log.LogInformation("The bot: {botinfo} is now ready --> ping time: {time}", sender.CurrentApplication.Name, sender.Ping);
	}
}