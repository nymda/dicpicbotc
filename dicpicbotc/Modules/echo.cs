﻿using System;
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
    public class echo : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo(params string[] text)
        {
            await ReplyAsync("Replaced with dp.vent");     
        }
    }
}
