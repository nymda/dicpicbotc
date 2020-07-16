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
    public class voat : ModuleBase<SocketCommandContext>
    {
        [Command("vote")]
        public async Task Voat(string id, string vote)
        {
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/polls";
            List<string> voters = new List<string> { };
            List<string> alpha = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            DirectoryInfo d = new DirectoryInfo(dppath);
            string fileLocation = "";
            bool foundFile = false;

            foreach (var file in d.GetFiles("*.txt"))
            {
                if(file.Name == id + ".txt")
                {
                    foundFile = true;
                    fileLocation = file.FullName;
                    voters = System.IO.File.ReadAllLines(dppath + "/" + id + "_voters.txt").ToList();
                    break;
                }
            }

            string voteLow = vote.ToLower();

            string[] polldata = System.IO.File.ReadAllLines(fileLocation);
            List<string> polldataList = System.IO.File.ReadAllLines(fileLocation).ToList();

            try
            {
                if (voters.Count != 0)
                {
                    for (int i = 0; i <= voters.Count; i++)
                    {
                        if (voters[i] == Context.Message.Author.Id.ToString())
                        {
                            await ReplyAsync("you have already voted on this poll.");
                            return;
                        }
                    }
                }
            }
            catch
            {

            }

            //what the fuck does this unholy mess do
            if (alpha.Contains(voteLow))
            {
                int position = alpha.IndexOf(voteLow);
                string[] current = polldata[position + 2].Split(",");
                current[1] = (Int32.Parse(current[1]) + 1).ToString();
                string modified = string.Join(",", current);
                polldataList[position + 2] = modified;
                System.IO.File.WriteAllLines(fileLocation, polldataList);
                await ReplyAsync("(" + id + ") " + current[0] + " now has " + current[1] + " votes!");
                voters.Add(Context.Message.Author.Id.ToString());
                System.IO.File.WriteAllLines(dppath + "/" + id + "_voters.txt", voters.ToArray());
                return;
            }


            if(foundFile == false)
            {
                await ReplyAsync("poll not found!");
                return;
            }


            int counter = 0;

            foreach (string s in polldata)
            {
                try
                {
                    string[] current = s.Split(",");
                    if(current[0] == vote)
                    {
                        current[1] = (Int32.Parse(current[1]) + 1).ToString();
                        string modified = string.Join(",", current);
                        polldataList[counter] = modified;
                        System.IO.File.WriteAllLines(fileLocation, polldataList);
                        await ReplyAsync("(" + id + ") " + vote + " now has " + current[1] + " votes!");
                        voters.Add(Context.Message.Author.Id.ToString());
                        System.IO.File.WriteAllLines(dppath + "/" + id + "_voters.txt", voters.ToArray());
                        break;
                    }
                }
                catch
                {

                }
                counter++;
            }
        }
    }
}
