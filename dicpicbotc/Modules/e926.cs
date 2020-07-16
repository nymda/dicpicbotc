using Discord;
using Discord.Commands;
using Newtonsoft.Json;
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
        public async Task E926(params string[] dat)
        {
            Post selected = e621dl(string.Join("%20", dat), 9999999, "");
            if (dat.Contains("dicpic"))
            {
                await ReplyAsync("Its me!");
            }
            await ReplyAsync("Boop! | Score: " + selected.score.total + " | Rating: " + obtainFullScore(selected.rating) + " | URL: " + selected.file.url);
        }

        public Random r = new Random();

        public string obtainFullScore(string sho)
        {
            switch (sho)
            {
                case "e":
                    return "Explicit";
                case "q":
                    return "Questionable";
                case "s":
                    return "Safe";
            }
            return "unknown";
        }
        public Post e621dl(string tags, int prevLastID, string appendLoginText)
        {
            WebClient w = new WebClient();
            w.Headers.Add("user-agent", getRandomHeader());
            byte[] dldata = w.DownloadData(@"https://e926.net/posts.json?limit=300&tags=" + tags + "+id%3A<" + prevLastID + appendLoginText);
            string dataraw = System.Text.Encoding.UTF8.GetString(dldata);
            dataraw = dataraw.Replace("\"has\":null", "\"has\":false");
            dataraw = dataraw.Replace("\"status_locked\":null", "\"status_locked\":false");
            RootObject parsedJson = JsonConvert.DeserializeObject<RootObject>(dataraw);
            var noCub = parsedJson.posts.Where(Post => !Post.tags.general.Contains("cub"));
            return noCub.ToArray()[r.Next(noCub.Count())];
        }

        public string getRandomHeader()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            return ("win621d_" + rand);
        }
    }
}
