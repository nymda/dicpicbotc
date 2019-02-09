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
    public class protogen : ModuleBase<SocketCommandContext>
    {
        [Command("proto")]
        public async Task Pnsfw(string flags = "", string fileURL = "")
        {
            Random r = new Random();

            int protocount = 0;
            List<string> protofiles = new List<string> { };
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/protogens";
            DirectoryInfo d = new DirectoryInfo(dppath);
            foreach (FileInfo f in d.GetFiles())
            {
                protofiles.Add(f.Name);
                protocount++;
            }

            if (flags == "-i")
            {
                protocount = 0;
                foreach (FileInfo f in d.GetFiles())
                {
                    protofiles.Add(f.Name);
                    protocount++;
                }
                await ReplyAsync("there are currently " + protocount + " protogens stored");
                return;
            }
            else if (flags == "-s")
            {
                try
                {
                    if (fileURL == "")
                    {
                        throw new Exception("no file url");
                    }

                    //submit
                    Random random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

                    WebClient w = new WebClient();

                    byte[] testData = w.DownloadData(fileURL);
                    string data = "";
                    data = BitConverter.ToString(testData);
                    string print = data.Substring(0, 11);

                    if((print == "FF-D8-FF-E0") || (print == "89-50-4E-47") || (print == "47-49-46-38"))
                    {
                        w.DownloadFile(fileURL, dppath + "/" + rand + ".png");

                        Console.WriteLine("[info] downloaded file into protogens");

                        protocount = 0;


                        foreach (FileInfo f in d.GetFiles())
                        {
                            protofiles.Add(f.Name);
                            protocount++;
                        }

                        await ReplyAsync("submitted succesfully! there are now " + protocount + " protogens stored");
                    }
                    else
                    {
                        await ReplyAsync("files must be in PNG, JPG or GIF format!");
                    }

                }
                catch
                {
                    await ReplyAsync("invalid command!");
                }
            }
            else
            {
                string[] titles = new string[] { "cute!", "roboboi", "*beep*", "*boop*", "fluff!" };

                int titleint = r.Next(titles.Length);

                int pictureint = r.Next(protocount);

                await Context.Channel.SendFileAsync(dppath + "/" + protofiles[pictureint], titles[titleint]);
            }

        }
    }
}
