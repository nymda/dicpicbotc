using Discord.Commands;
using System;
using System.Drawing;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace dicpicbotc.Modules
{
    public class illegal : ModuleBase<SocketCommandContext>
    {
        [Command("illegal")]
        public async Task Illegal(string text)
        {
            //profile image co-ordinates
            Point upper = new Point(77, 142);
            Point lower = new Point(423, 464);

            //rect for defining size of text
            Point strupper = new Point(77, 465);
            Size rectsize = new Size(346, 60);
            Rectangle r = new Rectangle(strupper, rectsize);
            Font font1 = new Font("Arial", 120, FontStyle.Regular, GraphicsUnit.Pixel);

            string TempPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/wantedTemplate.jpg";

            WebClient w = new WebClient();

            var user = Context.Message.MentionedUsers.FirstOrDefault();
            byte[] profileImg = w.DownloadData(user.GetAvatarUrl(Discord.ImageFormat.Auto, 1024).ToString());

            Bitmap pfp = (Bitmap)((new ImageConverter()).ConvertFrom(profileImg));
            Bitmap template = (Bitmap)Bitmap.FromFile(TempPath);
            Graphics g = Graphics.FromImage(template);

            g.DrawImage(pfp, 77, 142, 346, 322);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font f = FindFont(g, user.Username, r.Size, font1);
            //Font goodFont = FindFont(g, user.Username, r.Size, font1);
            g.DrawString(user.Username, f, Brushes.Red, r, stringFormat);

            template.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/" + "templatedrawn.png");

            await Context.Channel.SendFileAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/" + "templatedrawn.png");

            //await Context.Channel.SendFileAsync();
        }

        private Font FindFont(System.Drawing.Graphics g, string longString, Size Room, Font PreferedFont)
        {
            SizeF RealSize = g.MeasureString(longString, PreferedFont);
            float HeightScaleRatio = Room.Height / RealSize.Height;
            float WidthScaleRatio = Room.Width / RealSize.Width;

            float ScaleRatio = (HeightScaleRatio < WidthScaleRatio)
               ? ScaleRatio = HeightScaleRatio
               : ScaleRatio = WidthScaleRatio;

            float ScaleFontSize = PreferedFont.Size * ScaleRatio;

            return new Font(PreferedFont.FontFamily, ScaleFontSize);
        }
    }
}
