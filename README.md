# Discord Bot built with .Net
This project uses the [DSharp+](https://dsharpplus.github.io/DSharpPlus/) package and funktions as a starting point. <br />
Slah commands can be added anywhere within the project, be sure the command classes are public, you can find more information about how slash commands work in the documentation for the DSharp+ package.

## To get started
You need to add the file appsettings.json in the root folder and add this to it:

    "DiscordBotKey": "",
    "ServerID": ,
  
    "Serilog": {
        "MinimumLevel": {
            "Default": "Information",
            "Override": {
                "Microsoft": "Information",
                "System": "Warning"
            }
        }
    }
    
### Server specific or public slash commands
From the start all commands are server (guild) specific) <br />
If you want publicly added commands you need to change this line in BotService.cs <br />
`slash.RegisterCommands(Assembly.GetExecutingAssembly(), _serverID);` <br /> 
just remove _serverID as a parameter. <br />
_Note that when you add public commands it can take a while before they are added, as stated in the documentation._
