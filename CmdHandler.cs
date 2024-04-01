using Discord.WebSocket;
using dotNetDiscordBot.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetDiscordBot
{
	public static class CmdHandler
	{
		public static async Task execute(SocketSlashCommand command)
		{
			switch (command.Data.Name)
			{
				case "dot-curse":
					await DotCurse.Exec(command);
					break;
			}			
		}		
	}
}
