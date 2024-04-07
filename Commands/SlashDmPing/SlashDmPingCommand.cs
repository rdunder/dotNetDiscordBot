using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetDiscordBot.Commands.SlashDmPing;

public class SlashDmPingCommand : ApplicationCommandModule
{
	[SlashCommand("dot-DM-ping", "Command to test if the bot is awake, but in private message")]
	public async Task PingCommand(InteractionContext context)
	{
		var dmC = await context.Member.CreateDmChannelAsync();

		await dmC.SendMessageAsync("PONG, but in DM...");

		await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
			new DiscordInteractionResponseBuilder()
			.WithContent("Message Sent..."));
	}
}
