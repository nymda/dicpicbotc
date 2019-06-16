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
    public class avatar : ModuleBase<SocketCommandContext>
    {
        [Command("avatar")]
        public async Task Echo(string text = "")
        {
            WebClient w = new WebClient();
            Random r = new Random();

            if (text == "")
            {
                const string chars = "abcdefghijklmnopqrstuvwxyz1234567890";
                string ID = new string(Enumerable.Repeat(chars, 10).Select(s => s[r.Next(s.Length)]).ToArray());
                string avatarpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/avatars/";
                Console.WriteLine("[info] Downloaded avatar.");
                w.DownloadFile(Context.Message.Author.GetAvatarUrl(Discord.ImageFormat.Auto, 1024).ToString(), avatarpath + ID + ".png");
                await ReplyAsync("Heres your avatar!. Mention someone else for theirs.");
                await Context.Channel.SendFileAsync(avatarpath + ID + ".png");
            }
            else
            {
                const string chars = "abcdefghijklmnopqrstuvwxyz1234567890";
                string ID = new string(Enumerable.Repeat(chars, 10).Select(s => s[r.Next(s.Length)]).ToArray());
                var user = Context.Message.MentionedUsers.FirstOrDefault();
                string avatarpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/avatars/";
                Console.WriteLine("[info] Downloaded avatar.");
                w.DownloadFile(user.GetAvatarUrl(Discord.ImageFormat.Auto, 1024).ToString(), avatarpath + ID + ".png");

                if (user.Username.EndsWith('s'))
                {
                    await ReplyAsync("Heres " + user.Username + "' avatar!");
                }
                else
                {
                    await ReplyAsync("Heres " + user.Username + "'s avatar!");
                }

                await Context.Channel.SendFileAsync(avatarpath + ID + ".png");
            }
        }
    }
}
