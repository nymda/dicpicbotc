using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dicpicbotc.Modules
{
    public class ytdl : ModuleBase<SocketCommandContext>
    {
        [Command("ytdl")]
        public async Task Ytdl(string url)
        {
        //ah fuck it, maybe later
        }
    }
}