using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Threading;
using Discord.Commands;

namespace dicpicbotc.Modules
{
    public class version : ModuleBase<SocketCommandContext>
    {
        [Command("version")]
        public async Task Version()
        {
            await ReplyAsync("version: Release 0012");
        }
    }
}
