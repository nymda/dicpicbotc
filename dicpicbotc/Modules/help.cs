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
    public class help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Help()
        {
            string helpData = @"
            ```normal commands:
dp.help
dp.hug
dp.ping
dp.proto (-s submit, -i info)
dp.pnsfw (porn! | -s submit, -i info)
dp.e621 (porn! | -f fast mode)

CW+ specific commands:
dp.IPCget```";

            await ReplyAsync(helpData);
        }
    }
}
