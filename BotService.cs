using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace dotNetDiscordBot
{
	public sealed class BotService : IHostedService
	{
		private readonly ILogger _logger;
		private readonly IConfiguration _config;
		private readonly IHostApplicationLifetime _appLifetime;
		private readonly DiscordClient _client;

		private string _token;
		private ulong _serverID;

		public BotService(ILogger<BotService> logger, IConfiguration config, IHostApplicationLifetime appLifetime)
		{
			_logger = logger;
			_config = config;
			_appLifetime = appLifetime;

			_token = _config.GetValue<string>("DiscordBotKey") ?? throw new Exception("--> DiscordBotKey <-- is missing in appsettings.json");
			_serverID = _config.GetValue<ulong>("ServerID");

			_client = new(new()
			{
				Token = _token,
				TokenType = TokenType.Bot,
				Intents = DiscordIntents.AllUnprivileged
			});
        }

        public async Task StartAsync(CancellationToken cancellationToken)
		{
			_client.Ready += ClientReadyAsync;

			var slash = _client.UseSlashCommands();
			slash.RegisterCommands(Assembly.GetExecutingAssembly(), _serverID);

			await _client.ConnectAsync();
		}

		private async Task ClientReadyAsync(DiscordClient sender, ReadyEventArgs args)
		{
			var bot = await sender.GetCurrentApplicationAsync();
			_logger.LogInformation("The bot: {botinfo} is now ready --> ping time: {time}", bot.Name, sender.Ping);
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await _client.DisconnectAsync();
		}
	}
}
