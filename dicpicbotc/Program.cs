using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

//base by youtube codeforge

namespace dicpicbotc
{
    class Program
    {
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

            string bottoken = "NTM5OTE2NzY3MjI5NzcxODE0.DzOo2w.i-pfxRrJvJ_ijxVTwy7fX3pzjnw";

            dSocket.Log += Log;

            await RegisterCommandAsync();

            await dSocket.LoginAsync(Discord.TokenType.Bot, bottoken);

            await dSocket.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
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
                }
                if (result.IsSuccess)
                {
                    Console.WriteLine("[info] responded to {0}", message.Author);
                }
            }
        }
    }
}
