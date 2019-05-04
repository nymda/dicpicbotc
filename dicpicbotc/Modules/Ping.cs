using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace dicpicbotc.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("beep")]
        public async Task PingAsync()
        {
            await ReplyAsync("boop!");
        }
    }
}
