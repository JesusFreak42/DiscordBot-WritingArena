using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using WebSocket4Net;

namespace DiscordBot_WritingArena
{   
    class MyBot
    {

        private static void Main(string[] args) => new MyBot().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient disClient;
        private CommandService disCommands;
        private IServiceProvider disServices;

        public async Task RunBotAsync()
        {
            disClient = new DiscordSocketClient();
            disCommands = new CommandService();
            
            disServices = new ServiceCollection()
                .AddSingleton(disClient)
                .AddSingleton(disCommands)
                .BuildServiceProvider();

            string botToken = "NDI0NjAwMDA0MjI5ODU3Mjgy.DY8B7A.2rjMuKsd1ZejRr74JMRxqX8MbdA";

            //event subscriptions
            disClient.Log += Log;

            await RegisterCommandsAsync();

            await disClient.LoginAsync(TokenType.Bot, botToken);
            await disClient.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage x)
        {
            Console.WriteLine(x);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            disClient.MessageReceived += HandleCommandAsync;

            await disCommands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage x)
        {
            var message = x as SocketUserMessage;
            int argPos = 0;

            if (message is null || message.Author.IsBot) return;

            if (message.HasStringPrefix("+arena ", ref argPos) || message.HasMentionPrefix(disClient.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(disClient, message);
                var result = await disCommands.ExecuteAsync(context, argPos, disServices);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }

        }
    }
}


