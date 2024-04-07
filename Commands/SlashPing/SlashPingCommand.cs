using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;


namespace dotNetDiscordBot.Commands.SlashPing;

public class SlashPingCommand : ApplicationCommandModule
{
	[SlashCommand("dot-ping", "Command to test if the bot is awake")]
	public async Task PingCommand(InteractionContext context)
	{

		await context.CreateResponseAsync( InteractionResponseType.ChannelMessageWithSource,
			new DiscordInteractionResponseBuilder()
			.WithContent("Pong"));
	}
}
