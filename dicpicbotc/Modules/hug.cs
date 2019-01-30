using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Drawing;
using Discord.WebSocket;

namespace dicpicbotc.Modules
{
    public class hug : ModuleBase<SocketCommandContext>
    {
        [Command("hug")]
        public async Task Hug(string temp)
        {
            SocketUser sender = Context.Message.Author;
            var hugged = Context.Message.MentionedUsers.FirstOrDefault();

            if(hugged == null)
            {
                await ReplyAsync("you cant hug that!");
            }
            await ReplyAsync(sender.Mention + " hugged " + hugged.Mention);
        }

    }
}
