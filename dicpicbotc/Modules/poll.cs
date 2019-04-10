using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Threading;

namespace dicpicbotc.Modules
{
    public class poll : ModuleBase<SocketCommandContext>
    {
        [Command("poll")]
        public async Task Poll(string flags, params string[] data)
        {
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/polls";
            Random r = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            string ID =  new string(Enumerable.Repeat(chars, 5).Select(s => s[r.Next(s.Length)]).ToArray());

            string creatorid = (Context.Message.Author.Id).ToString();

            List<String> regionalAlphabet = new List<string> { "🇦", "🇧", "🇨", "🇩", "🇪", "🇫", "🇬", "🇭", "🇮", "🇯", "🇰", "🇱"};

            if(flags == "-new")
            {
                string holder = "";

                if (data.Length == 0)
                {
                    holder = "NO TITLE";
                }

                foreach(string s in data)
                {
                    holder = holder + s;
                }
                var b = File.Create(dppath + "/" + ID + ".txt");
                b.Dispose();
                File.WriteAllText(dppath + "/" + ID + ".txt", creatorid + "\n" + holder);
                var f = File.Create(dppath + "/" + ID + "_voters.txt");
                var msg = await ReplyAsync("new poll created. ID: " + ID);
                f.Dispose();
                b.Dispose();
            }

            if (flags == "-list")
            {
                string holder = "";
                DirectoryInfo pathinfo = new DirectoryInfo(dppath);
                foreach (FileInfo File in pathinfo.GetFiles())
                {
                    if(!File.Name.Contains("_"))
                    {
                        string file = (File.Name).Replace(".txt", "");
                        holder = holder + file + "\n";
                    }

                }

                var msg = await ReplyAsync("current polls IDs:\n" + holder);
            }

            if (flags == "-add")
            {
                string id = data[0];
                string holder = "";
                for(int i = 1; i < data.Length; i++)
                {
                    holder = holder + " " + data[i];
                }

                try
                {
                    string[] currentpoll = File.ReadAllLines(dppath + "/" + id + ".txt");
                    if(currentpoll[0] != creatorid)
                    {
                        await ReplyAsync("You cannot edit that poll.");
                        return;
                    }
                    holder = holder + ",0";
                    List<string> currentpolledit = currentpoll.ToList();
                    currentpolledit.Add(holder);
                    currentpoll = currentpolledit.ToArray();
                    File.WriteAllLines(dppath + "/" + id + ".txt", currentpoll);
                }
                catch
                {
                    await ReplyAsync("poll not found");
                    return;
                }

                var msg = await ReplyAsync("adding: " + holder.Substring(0, holder.Length - 2));
            }
        }
    }
}
