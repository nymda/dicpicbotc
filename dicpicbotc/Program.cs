using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

//base by youtube: codeforge

namespace dicpicbotc
{
    class Program
    {
        string TokenPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/token.txt";

        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient dSocket;
        private CommandService dComm;
        private IServiceProvider services;

        public async Task RunBotAsync()
        {
            dSocket = new DiscordSocketClient();
            dComm = new CommandService();
            services = new ServiceCollection()
                .AddSingleton(dSocket)
                .AddSingleton(dComm)
                .BuildServiceProvider();

            string bottoken = File.ReadAllText(TokenPath);
            Console.WriteLine(bottoken);
            dSocket.Log += Log;
            await RegisterCommandAsync();
            await dSocket.LoginAsync(Discord.TokenType.Bot, bottoken);
            await dSocket.StartAsync();
            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine("[info] " + arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            dSocket.MessageReceived += handleCommandAsync;
            await dComm.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }

        private async Task handleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message is null || message.Author.IsBot) return;
            int arguentPosition = 0;
            if (message.HasStringPrefix("dp.", ref arguentPosition) || message.HasMentionPrefix(dSocket.CurrentUser, ref arguentPosition))
            {
                var context = new SocketCommandContext(dSocket, message);
                var result = await dComm.ExecuteAsync(context, arguentPosition, services);
                if (!result.IsSuccess)
                {           
                    Console.WriteLine(result.ErrorReason);
                    if(result.ErrorReason == "This command may only be invoked in an NSFW channel.")
                    {
                        Console.WriteLine("[info] responded to {0}", message.Author);
                        await message.Channel.SendMessageAsync("That can only be run in a NSFW channel.");
                    }
                    else if(result.ErrorReason == "Unknown command.")
                    {
                        Console.WriteLine("[info] responded to {0}", message.Author);
                        await message.Channel.SendMessageAsync("I dont know that command.");
                    }
                    else
                    {
                        Console.WriteLine("[info] responded to {0}", message.Author);
                        await message.Channel.SendMessageAsync("error! tell kned about this.");
                    }
                }
                if (result.IsSuccess)
                {
                    Console.WriteLine("[info] responded to {0}", message.Author);
                }
            }
        }
    }
}
