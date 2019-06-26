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
    public class e926 : ModuleBase<SocketCommandContext>
    {
        [Command("e926")]
        public async Task E926(string flags = "", string tag1 = "", string tag2 = "", string tag3 = "", string tag4 = "", string tag5 = "")
        {
            bool isCub = false;

            string tag6 = "order:random";

            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/e926";

            List<string> e926list = new List<string> { };

            tag5 = tag4;
            tag4 = tag3;
            tag3 = tag2;
            tag2 = tag1;
            tag1 = flags;

            string prestring = "https://e926.net/post/index.json?limit=10&tags=";
            WebClient w = new WebClient();
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            w.Headers.Add("user-agent", "dicpicbot");
            Console.WriteLine("[info] e926 searching: " + tag1 + " " + tag2 + " " + tag3 + " " + tag4 + " " + tag5);
            byte[] e6d = w.DownloadData(prestring + tag1 + "," + tag2 + "," + tag3 + "," + tag4 + "," + tag5 + "," + tag6);
            if(e6d.Length == 2)
            {
                await ReplyAsync("**No results!**");
                return;
            }
            string e6draw = Encoding.UTF8.GetString(e6d);
            string[] datarawsplit = e6draw.Split(new string[] { "]}" }, StringSplitOptions.None);
            for (int i = 0; i < datarawsplit.Count(); i++)
            {
                string[] current = datarawsplit[i].Split(',');
                for (int o = 0; o < current.Count(); o++)
                {
                    if (current[o].Contains("tags\""))
                    {
                        string[] tags = current[o].Split(' ');
                        foreach(string tag in tags)
                        {
                            if(tag == "cub")
                            {
                                isCub = true;
                            }
                        }
                    }
                    if (current[o].Contains("file_url"))
                    {
                        string[] current2 = current[o].Split(new string[] { "\":\"" }, StringSplitOptions.None);
                        string final = current2[1].Replace("\"", string.Empty);
                        e926list.Add(final);
                    }
                }
            }

            Random r = new Random();

            int e926int = 0;
            string[] e6name = (e926list[e926int]).Split("/");
            if (!(isCub))
            {
                var msg = await ReplyAsync("**not yiff!**\n" + e926list[e926int]);
                //File.WriteAllText(dppath + "/lastmsg.txt", msg.Id.ToString() + "," + Context.Message.Author.Id.ToString());
            }
            else
            {
                await Context.Channel.SendFileAsync(dppath + "/hansen.jpg", "That image contained cub.");
            }


            //old content for non-fast mode

            //if(flags == "-f")
            //{
            //    await ReplyAsync("**fast mode! **" + e926list[e926int]);
            //    return;
            //}

            //w.DownloadFile(e926list[e926int], dppath + "/" + e6name[6]);
            //await Context.Channel.SendFileAsync(dppath + "/" + e6name[6], "*magic porn robot.*");
        }
    }
}
