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
            DirectoryInfo d = new DirectoryInfo(dppath);
            string fileLocation = "";
            bool foundFile = false;

            foreach (var file in d.GetFiles("*.txt"))
            {
                if(file.Name == id + ".txt")
                {
                    foundFile = true;
                    fileLocation = file.FullName;
                    voters = File.ReadAllLines(dppath + "/" + id + "_voters.txt").ToList();
                    break;
                }
            }


            if(voters.Count != 0)
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


            if(foundFile == false)
            {
                await ReplyAsync("poll not found!");
                return;
            }

            string[] polldata = File.ReadAllLines(fileLocation);
            List<string> polldataList = File.ReadAllLines(fileLocation).ToList();

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
                        File.WriteAllLines(fileLocation, polldataList);
                        await ReplyAsync("(" + id + ") " + vote + " now has " + current[1] + " votes!");
                        voters.Add(Context.Message.Author.Id.ToString());
                        File.WriteAllLines(dppath + "/" + id + "_voters.txt", voters.ToArray());
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
