﻿using System.Linq;
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
    public class help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Help()
        {
            string helpData = @"
            ```normal commands:
dp.help (displays this page)
dp.hug (hugs a user)
dp.echo (echos the users message back to them)
dp.vent (echos the users message and removes the origional, making the message private)
dp.ping (tests if im online. if you can read this, i am)
dp.proto (-s submit, -i info)
dp.pnsfw (porn! | -s submit, -i info)
dp.e621 (porn!)
dp.e926 (not porn!
dp.hansen (removes the last e621 message, for use when cub appears)
dp.roll (rolls a dice. [X]d[Y])
dp.avatar (gets your, or another users avatar)
dp.fact (prints a random fact)
dp.version (displays the bots version)
dp.illegal (creates wanted poster thing)
dp.prima (generates an illegal primagen. shhhhhh~)

dp.poll (creates a new poll)
dp.results (displays the results of a poll)
dp.vote (votes on a poll)

CW+ specific commands:
dp.IPCget```";

            await ReplyAsync(helpData);
        }
    }
}
