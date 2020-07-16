using Discord;
using Newtonsoft.Json;
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
    public class e621 : ModuleBase<SocketCommandContext>
    {
        [RequireNsfw]
        [Command("e621")]
        public async Task E621(params string[] dat)
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
            byte[] dldata = w.DownloadData(@"https://e621.net/posts.json?limit=300&tags=" + tags + "+id%3A<" + prevLastID + appendLoginText);
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
    public class File
    {
        public int width { get; set; }
        public int height { get; set; }
        public string ext { get; set; }
        public int size { get; set; }
        public string md5 { get; set; }
        public string url { get; set; }
    }

    public class Preview
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Sample
    {
        public bool has { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string url { get; set; }
    }

    public class Score
    {
        public int up { get; set; }
        public int down { get; set; }
        public int total { get; set; }
    }

    public class Tags
    {
        public List<string> general { get; set; }
        public List<string> species { get; set; }
        public List<object> character { get; set; }
        public List<object> copyright { get; set; }
        public List<object> artist { get; set; }
        public List<object> invalid { get; set; }
        public List<object> lore { get; set; }
        public List<object> meta { get; set; }
    }

    public class Flags
    {
        public bool pending { get; set; }
        public bool flagged { get; set; }
        public bool note_locked { get; set; }
        public bool status_locked { get; set; }
        public bool rating_locked { get; set; }
        public bool deleted { get; set; }
    }

    public class Relationships
    {
        public int? parent_id { get; set; }
        public bool has_children { get; set; }
        public bool has_active_children { get; set; }
        public List<object> children { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public File file { get; set; }
        public Preview preview { get; set; }
        public Sample sample { get; set; }
        public Score score { get; set; }
        public Tags tags { get; set; }
        public List<object> locked_tags { get; set; }
        public int change_seq { get; set; }
        public Flags flags { get; set; }
        public string rating { get; set; }
        public int fav_count { get; set; }
        public List<object> sources { get; set; }
        public List<object> pools { get; set; }
        public Relationships relationships { get; set; }
        public int? approver_id { get; set; }
        public int uploader_id { get; set; }
        public string description { get; set; }
        public int comment_count { get; set; }
        public bool is_favorited { get; set; }
    }

    public class RootObject
    {
        public List<Post> posts { get; set; }
    }
}
