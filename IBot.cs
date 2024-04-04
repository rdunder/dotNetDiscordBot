using Microsoft.Extensions.DependencyInjection;

namespace dotNetDiscordBot;

public interface IBot
{
	Task StartAsync();
}
