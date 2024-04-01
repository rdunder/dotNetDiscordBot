using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Discord;
using Discord.WebSocket;
using Discord.Net;
using dotNetDiscordBot;

public class Program
{
	private static DiscordSocketClient? client;
	private static IConfiguration config;

	static string discordKey;
	static ulong serverID;

	static async Task Main(string[] args)
	{
		//	init and setting up config
		config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", false, true)
			.Build();

		//	Get values from appsetting.json
		discordKey = config["DiscordBotKey"] ?? "";
		serverID = Convert.ToUInt64(config["serverID"]);

		//	Create and set up the Discord client
		client = new DiscordSocketClient();
		client.Log += Log;
		client.Ready += ReadyAsync;
		client.SlashCommandExecuted += CmdHandler.execute;
		
		//	Start the discord bot
		await client.LoginAsync(TokenType.Bot, discordKey);
		await client.StartAsync();

		//	Block this task until this program is killed
		await Task.Delay(-1);
	}


	static async Task ReadyAsync()
	{
		// init guild (server) from the server id, and deleting previous commands
		var server = client.GetGuild(serverID);
		await server.DeleteApplicationCommandsAsync();

		// Create slash commands
		var curseCommand = new SlashCommandBuilder();

		// Setting names for the commands
		curseCommand.WithName("dot-curse");

		// Setting descriptions for the commands
		curseCommand.WithDescription("Makes the bot curse");


		try
		{
			// Build the commands
			await server.CreateApplicationCommandAsync(curseCommand.Build());
		}
		catch (Exception ex)
		{
			Console.WriteLine("An Error occured! here is the error message:\n" + ex.Message);
		}
	}


	static Task Log(LogMessage logMsg)
	{
		Console.WriteLine(logMsg.ToString());
		return Task.CompletedTask;
	}
}