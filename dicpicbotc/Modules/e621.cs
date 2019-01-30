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
    public class e621 : ModuleBase<SocketCommandContext>
    {
        [RequireNsfw]
        [Command("e621")]
        public async Task E621(string flags = "", string tag1 = "", string tag2 = "", string tag3 = "", string tag4 = "", string tag5 = "")
        {

            string tag6 = "order:random";

            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/e621";

            List<string> e621list = new List<string> { };

            await ReplyAsync("*processing*");

            if(flags != "-f")
            {
                tag1 = flags;
            }

            string prestring = "https://e621.net/post/index.json?limit=10&tags=";
            WebClient w = new WebClient();
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            w.Headers.Add("user-agent", "dicpicbot");
            Console.WriteLine(prestring + tag1 + "," + tag2 + "," + tag3 + "," + tag4 + "," + tag5 + "," + tag6);
            byte[] e6d = w.DownloadData(prestring + tag1 + "," + tag2 + "," + tag3 + "," + tag4 + "," + tag5 + "," + tag6);
            string e6draw = Encoding.UTF8.GetString(e6d);
            string[] datarawsplit = e6draw.Split(new string[] { "]}" }, StringSplitOptions.None);
            for (int i = 0; i < datarawsplit.Count(); i++)
            {
                string[] current = datarawsplit[i].Split(',');
                for (int o = 0; o < current.Count(); o++)
                {
                    if (current[o].Contains("file_url"))
                    {
                        string[] current2 = current[o].Split(new string[] { "\":\"" }, StringSplitOptions.None);
                        string final = current2[1].Replace("\"", string.Empty);
                        e621list.Add(final);
                        //Console.WriteLine(final);
                    }
                }
            }

            Random r = new Random();

            int e621int = r.Next(e621list.Count);

            string[] e6name = (e621list[e621int]).Split("/");

            if(flags == "-f")
            {
                await ReplyAsync("**fast mode! **" + e621list[e621int]);
                return;
            }

            w.DownloadFile(e621list[e621int], dppath + "/" + e6name[6]);

            await Context.Channel.SendFileAsync(dppath + "/" + e6name[6], "*magic porn robot.*");
        }
    }
}
