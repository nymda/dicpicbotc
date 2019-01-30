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
    public class pnsfw : ModuleBase<SocketCommandContext>
    {
        [RequireNsfw]
        [Command("pnsfw")]
        public async Task Pnsfw(string flags = "", string fileURL = "")
        {
            Random r = new Random();

            int protocount = 0;
            List<string> protofiles = new List<string> { };
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/nsfwprotogens";
            DirectoryInfo d = new DirectoryInfo(dppath);
            foreach (FileInfo f in d.GetFiles())
            {
                protofiles.Add(f.Name);
                protocount++;
            }

            if(flags == "-i")
            {
                protocount = 0;
                foreach (FileInfo f in d.GetFiles())
                {
                    protofiles.Add(f.Name);
                    protocount++;
                }
                await ReplyAsync("there are currently " + protocount + " nsfw protogens stored");
                return;
            }
            else if(flags == "-s")
            {
                try
                {
                    if(fileURL == "")
                    {
                        throw new Exception("no file url");
                    }

                    //submit
                    Random random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

                    WebClient w = new WebClient();
                    w.DownloadFile(fileURL, dppath + "/" + rand + ".png");

                    Console.WriteLine("[info] downloaded file into nsfwprotofens");

                    protocount = 0;
                    foreach (FileInfo f in d.GetFiles())
                    {
                        protofiles.Add(f.Name);
                        protocount++;
                    }

                    await ReplyAsync("submitted succesfully! there are now " + protocount + " nsfw protogens stored");
                }
                catch
                {
                    await ReplyAsync("invalid command!");
                }
            }
            else
            {
                string[] titles = new string[] { "lewd boye", "i thought this wasnt allowed", "robot porn", "how 2 get blacklisted", "*aroused beep*" };

                int titleint = r.Next(titles.Length);

                int pictureint = r.Next(protocount);

                await Context.Channel.SendFileAsync(dppath + "/" + protofiles[pictureint], titles[titleint]);
            }

        }
    }
}
