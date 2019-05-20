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
            File.AppendAllText(dppath, "\nVent message at " + DateTime.Now.ToString("h:mm:ss tt") + " from " + Context.Message.Author + " with ID " + ID);
            string text_str = string.Join(" ", text);
            await Context.Message.DeleteAsync();
            await ReplyAsync(text_str + "\n" + "`ID: " + ID + "`");
        }
    }
}
