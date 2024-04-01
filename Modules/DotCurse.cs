using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetDiscordBot.Modules
{
	public static class DotCurse
	{
		string[] curses = new string[]
		{

		};
		public static async Task Exec(SocketSlashCommand command)
		{
			await command.RespondAsync("Fluffy Rainbows !");
		}
	}
}
