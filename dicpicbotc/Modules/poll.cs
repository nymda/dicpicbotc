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
        public async Task Poll(params string[] objects)
        {
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/polls";
            Random r = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            string ID =  new string(Enumerable.Repeat(chars, 5).Select(s => s[r.Next(s.Length)]).ToArray());

            List<String> regionalAlphabet = new List<string> { "🇦", "🇧", "🇨", "🇩", "🇪", "🇫", "🇬", "🇭", "🇮", "🇯", "🇰", "🇱"};

            var builder = new EmbedBuilder();
            string joined = string.Join(' ', objects);
            string[] titleObjects = joined.Split("|");
            string title = titleObjects[0];
            string[] objectsSplit = titleObjects[1].Split(' ');

            if(objectsSplit.Length > 11)
            {
                await ReplyAsync("please use 12 or less items.");
                return;
            }

            builder.WithTitle(title);

            objectsSplit = objectsSplit.Skip(1).ToArray();

            int counter = 0;
            int counter2 = 0;

            string responce = title + "| (ID: " + ID + ")\n";

            foreach (string i in objectsSplit)
            {
                responce = responce + regionalAlphabet[counter] + " " + i + "\n";
                counter++;
            }

            var msg = await ReplyAsync(responce);

            foreach (string i in objectsSplit)
            {
                await msg.AddReactionAsync(new Emoji(regionalAlphabet[counter2]));

                counter2++;
            }

            File.WriteAllText(dppath + "/" + ID + ".txt", msg.Id.ToString());
        }
    }
}
