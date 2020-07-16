using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Linq;


namespace dicpicbotc.Modules
{
    public class hansen : ModuleBase<SocketCommandContext>
    {
        [Command("hansen")]
        public async Task Hansen()
        {
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/e621";
            string msgkey = System.IO.File.ReadAllText(dppath + "/lastmsg.txt");
            string[] data = msgkey.Split(",");

            if(Context.Message.Author.Id.ToString() == data[1])
            {
                try
                {
                    await Context.Channel.DeleteMessageAsync(ulong.Parse(data[0]));
                    await Context.Channel.SendFileAsync(dppath + "/hansen.jpg", "Seat taken.");

                }
                catch
                {
                    await ReplyAsync("Message does not exist.");
                }
            }
            else
            {
                await ReplyAsync("Thats not yours.");
            }
        }
    }
}
