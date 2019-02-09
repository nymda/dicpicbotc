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

    public class roll : ModuleBase<SocketCommandContext>
    {
        [Command("roll")]
        public async Task RollDice(string number = "", string args = "")
        {
            if(number == "")
            {
                await ReplyAsync("command usage: dp.roll [x]d[y] where x is the number of dice and y is the number of sides.");
                return;
            }

            Random r = new Random();
            try
            {
                string[] data = number.Split('d');
                int diceNum = Int32.Parse(data[0]);
                int sideNum = Int32.Parse(data[1]);

                if(diceNum > 100 || sideNum > 100)
                {
                    await ReplyAsync("these dice are too big for my hands...");
                    return;
                }

                List<int> dices = new List<int> { };

                int total = 0;
                int current = 0;

                for(int i = 0; i <= diceNum; i++)
                {
                    current = r.Next(1, sideNum + 1);
                    dices.Add(current);
                    total = total += current;
                }

                string output = "[";

                foreach(int i in dices)
                {
                    output = output + i + ", ";
                }

                output = output.Remove(output.Length - 2);

                output = output + "] **" + total + "**";

                if(args == "-s")
                {
                    await ReplyAsync(":game_die: **" + total + "**");
                }
                else
                {
                    await ReplyAsync(":game_die: " + output);
                }


            }
            catch
            {
                await ReplyAsync("invalid command usage!");
            }
        }
    }
}
