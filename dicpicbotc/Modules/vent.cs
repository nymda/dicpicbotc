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
    public class vent : ModuleBase<SocketCommandContext>
    {
        [Command("vent")]
        public async Task Vent(params string[] text)
        {
            Random r = new Random();
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/log.txt";
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            string ID = new string(Enumerable.Repeat(chars, 5).Select(s => s[r.Next(s.Length)]).ToArray());
            string text_str = string.Join(" ", text);
            System.IO.File.AppendAllText(dppath, text_str + " Was vented by " + Context.Message.Author);
            await Context.Message.DeleteAsync();
            await ReplyAsync(text_str + "\n" + "`ID: " + ID + "`");
        }
    }
}
