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
            List<string> hugMessages = new List<String> { " hugged ", " snugged ", " tacklesnugs ", " cuddles " };

            Random r = new Random();

            int hugInt = r.Next(hugMessages.Count);

            SocketUser sender = Context.Message.Author;
            var hugged = Context.Message.MentionedUsers.FirstOrDefault();

            if(hugged == null)
            {
                await ReplyAsync("you cant hug that!");
            }

            if ((hugged.IsBot) && (hugged.Username != "dicpicBot"))
            {
                await ReplyAsync(sender.Mention + " tried to hug " + hugged.Mention + " but im the only bot allowed to be cuddled!");
                return;
            }

            await ReplyAsync(sender.Mention + hugMessages[hugInt] + hugged.Mention);
        }

    }
}
