using DSharpPlus.SlashCommands;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetDiscordBot;

public class SlashCommands : ApplicationCommandModule
{

	[SlashCommand("dot-ping", "Command to test if the bot is awake")]
	public async Task PingCommand(InteractionContext context)
	{
		await context.CreateResponseAsync(
			InteractionResponseType.ChannelMessageWithSource,
			new DSharpPlus.Entities.DiscordInteractionResponseBuilder()
			.WithContent("Pong"));
	}
}
