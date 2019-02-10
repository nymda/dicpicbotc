using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Threading;
using Discord.Commands;

namespace dicpicbotc.Modules
{
    public class results : ModuleBase<SocketCommandContext>
    {
        [Command("results")]
        public async Task Results(string id)
        {
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/polls";
            List<String> regionalAlphabet = new List<string> { "🇦", "🇧", "🇨", "🇩", "🇪", "🇫", "🇬", "🇭", "🇮", "🇯", "🇰", "🇱" };
            DirectoryInfo d = new DirectoryInfo(dppath);
            bool foundFile = false;
            List<string> data = new List<string> { };
            string output = "";

            foreach (var file in d.GetFiles("*.txt"))
            {
                if (file.Name == id + ".txt")
                {
                    foundFile = true;
                    data = File.ReadAllLines(file.FullName).ToList();
                    break;
                }
            }

            if(foundFile == false)
            {
                await ReplyAsync("poll not found!");
                return;
            }

            output = "results for " + id + "\n\n**" + data[1] + "**\n";
            for(int i = 2; i < data.Count; i++)
            {
                output = output + regionalAlphabet[i - 2] + " **" + data[i] + "**\n";
            }

            output = output.Replace(",", ": ");

            await ReplyAsync(output);
        }
    }
}
