using DSharpPlus.SlashCommands;
using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;

namespace dotNetDiscordBot;

public class SlashCommands : ApplicationCommandModule
{

    [SlashCommand("dot-ping", "Command to test if the bot is awake")]
	public async Task PingCommand(InteractionContext context)
	{
		await context.CreateResponseAsync(
			InteractionResponseType.ChannelMessageWithSource,
			new DiscordInteractionResponseBuilder()
			.WithContent("Pong"));		
	}
}
