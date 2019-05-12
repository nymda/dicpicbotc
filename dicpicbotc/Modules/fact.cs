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
    public class fact : ModuleBase<SocketCommandContext>
    {
        [Command("fact")]
        public async Task Fact()
        {
            Random r = new Random();
            int choice = r.Next(0, 2);
            Console.WriteLine(choice);
            if(choice == 0)
            {
                WebClient w = new WebClient();
                byte[] dat = w.DownloadData("http://numbersapi.com/random");
                string fact = Encoding.UTF8.GetString(dat);
                await ReplyAsync("`" + fact + "`");
            }
            if(choice == 1)
            {
                WebClient w = new WebClient();
                byte[] dat = w.DownloadData("https://randomuselessfact.appspot.com/random.json?language=en");
                string raw = Encoding.UTF8.GetString(dat);
                string[] split = raw.Split(new string[] { "\"," }, StringSplitOptions.None); ;
                string fact = split[0];
                fact = string.Join(string.Empty, fact.Skip(10));
                fact = fact.Remove(fact.Length - 1);
                await ReplyAsync("`" + fact + "`");
            }
        }
    }
}
