using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dicpicbotc.Modules
{
    public class primagen : ModuleBase<SocketCommandContext>
    {
        public const int _width = 260;
        public const int _height = 305;
        public const string prima = "iVBORw0KGgoAAAANSUhEUgAAAQQAAAExCAMAAACtYSwmAAAABGdBTUEAALGPC/xhBQAAAwBQTFRFAAAAOzJ0TlJdYGVva3B7IrFM+rI8//IAqNrC2eDi8Pf5AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAsjkizQAAAQB0Uk5T////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AFP3ByUAAAAJcEhZcwAADsEAAA7BAbiRa+0AAAAZdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuMjHxIGmVAAAKbElEQVR4Xu2d7Y4jRw4Ez2ff2r73f9+96MoUj+ovdUua1tjO+NFgsVhVZAA7O4AX8L9+hp+RAJEAkQCRAJEAkQCRAJEAkQB/FQm/3PD6rUQCRAL8hST8Z/AVHiIBIgG+pwQm7cMSy4DoW28hEiAS4CMSxogTXq9N/euvv1aGwPPf6MWvEwkQCfApCf8eEGgcvpp6tlQMHr6h3bcQCRAJ8FkJYkw5weClYhbz9fANkr7xNSIBIgG+iYQfP35oqEISoGKCXjM0vMdDJEAkwGUS1HnFnv8GGXkoRrkpCcBWHdGSr659mkiASIArJfz222+j8V/++OMPvpqlIDPzIMaJCXkgo3qKdUo1fuYpIgEiAS6TALSKh1KhWTokVz0InSJw9fs8RAJEAnyphNG10VISBEvN0iG54wGmu8bU4DMve4gEiAT4agke+PZzALwesPQYN8jsSxDjpjsVyvjhk0QCRAJcJqGYJVlqCsFyVQL5LapAx/32GSIBIgG+VALQladtTK23PLEMgLbwMFPRa2boCNTSbx8mEiAS4CMSltQIYhrpXsVqgRcL2PLzx4gEiAT4aglASx51lzHWporZllhNCrb8/AEiASIBvo8EQTF4koEy4PWCrS3y7uARkQCRABdIAPrxkPdMs23gSRqrSbFV7+cfEQkQCXCNBKAlT36jOudvQP1a2GFXqAZ6vGR1l6Sf3yUSIBLgMglAS55/0NuefincVSFcPdAR8PoFD5EAkQAXSBjtrzDzAJ7qBrt2MZgVC1V6seaBjPvYJhIgEuAaCdOPgA1WRwPyGlzse3A0WNaQcSsbRAJEAlwgAWjDM6/BLrjjG7MMy4MSYHnWfWwQCfD9JWhEln91CUAn4LHXUEFnTMCI04wstySsMqtk6T7W+O4SJgeTBZaRwPJvIEHQTMfzr8Hu1P2GhJE7AQfdwRqRAJEAF0sA+gGPugtlsqBBZj8Tpo0NFat5zrqDBd9eAhNpiH+0hOJvI+Hg+EVNPTMg5AG8vlEZjtQpAjexIBIgEuBKCbTh2Rqryc40xoBY4yxZeljCDe5jQSRAJMDFEpZo0oN4ngVLCdzsaMDSTawRCRAJcKWETh9/cnFb9vwqFHiwxkyCakgqz9KvbhAJEAnwEQl90tnUs+UWGrOjkYEtBUqy9KvbRAJEAnxWwnLkZWYLKkEGip5RgZ/cJRIgEuBTEoTnaawmd9A9HvuGkn7sAJEAkQAfkSDo05Pcs5VfRcXTzA0/cJhIgEiAT0mgVU2xpLZO1RD46vNEAkQClAQuEV5/MTyk5lfR7qkaAl99nkiASABJ4IY///zz9duOU82vQjNqaVbWl8Silr76PJEAkQBLCfDKhQep5lehGTEr68s39hwJEAmwKgFeufMIfZwOeRgCTK+cxctd336SSIBIgC4BfN3g6TuP0JvvVCcdFfNV0MsqA8S+/SSRAJEAOxKA/NM378O1fuMe8upkhvJTNwMlgdgnI2FA7NtPEgkwkwC+sTG9euZ+1YPXa7Dr2+8h7z6O0e/Zf3GHSIBIgCMSBDX7jJsmiOuIUwuqZgZ593EPeeH1DTI+GQkDYt9+kkgASYB+vy89ydRio5K6XEHBUgUzyLuJBsn/Dpa7/R5i4TcOEwkQCfBGCTO4sALFfP3YGQlkGF//NHHmoV8yi6EeElquEgkQCbAqAXzdm+DyCuo5ZTokhZsYYwr9iyw8EHvvXsKScW46qEDo9RmRAJEAJQGo8e0D3/UmuLwCvaWlYNkzWoJmB8WzHwsEPrCBTs3u0bCdSIBIgB0J4LveDQ+BFwdQvSgJfL29zTjxfyIBIgGOSABqNH7h694NDzk6wPSH+UZ1ePAGynxynAWP2ogEiASYSQDK9ErHN74JnqjvQTzGTYJmeXgDBcKHx3GN2YkEiARYSgAqNXvH974Ml8+CI1As6ERBtdpx9Q0yNbtivjrYiQSIBFiVABRr9iV+4Cm41tEZCRoE1NXqIIKtQmcJ6iy47p5IgEiALQmgUxp8iZ44hS70YqAMeL2NyvQ0gVvcRUc63lgQCRAJsCNB6Lhef4g7bui4cGoNVyzQbv2hrle06xZfJhIgEuChBKFHQW2sooZncMTRAXxRQ48Kp24POfsOFZEAkQAHJRR+eGFDjRU9Q7GjR+iqI/jAuNydvUAkQCTAWQnFMDHhtu5xi6NJqFjBKj55DOp1s7t5jUiASIAnJOj1jju7oaFAu148wocPwxEud0+vEQkQCfCcBPfxiG5gy0blVa+DD1GljrutF4gEiAR4QgIc6bamI+jxMoA6omCHXly4rWeJBIgEeE4C+Pnttr096PPOYr76/7noiM7uMB27FXNQELutp4gEiAR4WkIxmppQk0vY6sP2WIHQEnxsAwqqWAcFSzd0nkiASIDXJQANuMU1Rr9uuMfQY0HGxzZYHhHk3c15IgEiAV6UwNPg/rahplrlq/+2CJXp7Fy4LO6w67ZOEgkQCfC6BPd3T89X5wo8/43KF1t3wqxyBrtu6ySRAJEAL0oAnp7hjm+QqSb5evgblS+WN4hZ2SrUuK0zRAJEArwuoTPrn6VQe3w9eaO2OmR8RWNZtoQat3KGSICPSPgxUFxbHTK+orEsW0KNWznD2yV0Kqn2+I655cAWaqtDxpPfWNZsQaXePc4HJFgB/HMlWMDgbylhFfWv7xMSiJcFO1Dshw8TCRAJ8G0lFJVR8BAq/fBhrpfQLEyr2uqQKSqj4CFU+uHDfEBCWRiL2uqQKSqj4CFU+uHDfEKCLSiurU4N0reWZVvU8YN8REKntjo1Rd9alm1Rxw8SCfDNJUDtLsu26MePEAkQCXCZBPXG18MPKjmjT9ELVouX9ONHiASIBLheApQBWJ1rNkWvWa2fMTv+kEiASIALJEB1PhthdaLVEXrl6qli9fg+kQCRABdLAOLCqXvI+9g9vX6cPnd8h0iASIDrJTxka4pp7IHr1nwSuPoMkQCRAN9Nws4UbOlXTQLwgRtKuvQkkQCRABdIoDd17nZ32RqkLhkaJog7rnuKSIBIgK+WoA6rbY+6AQU+dk/dAD0WW6eOEwkQCXCBBPVZDYNnbijvMwu0qxtAy0IZlz5FJEAkwMUSxNT6Pa7egILff/9dlb6iobxLnyISIBLgGgnVquKCjOseQWV5mEFeWy49TyRAJMAFEsAz31R0XPcIKjXsDsdvmxEJEAlwgQS1Zwv3nGp75oGl8HrA0tVniASIBPhqCaA+R78rqKDQkVXY1aRAbI/DpLOD/UtWiQSIBLhMwhZ9HNgfoa5anlJesASfOUAkQCTAd5MAO/3rKtfdU68QgDI+9ohIgEiACyQA/ajDVdSz2q7AJxdUTaEMXzFL+tgukQCRAN9BAlTPFfjkAhUULHVcKFNb4GO7RAJEAnxcgloFta1iBVuwqzFBxXVVJWvLZ3aJBIgEuEYC0E+1qlgBVMPg6ke4ukkrnrgtEiAS4CMS1CffWeDS1+Ces1dFAkQCXCYB1B7U1D3g67rLiQSIBLhSQlFT9wC8fTmRAJEAn5IgKlb+U0QCRAJ8RMJ3IxIgEiASIBIgEiASfv78+T/v9iJ8FpoInQAAAABJRU5ErkJggg==";

        [Command("prima")]
        public async Task Primagen(string dim = "1x1")
        {
            if(dim != "1x1")
            {
                await Context.Channel.SendMessageAsync("Hold on, processing...");
            }

            String[] dimr = dim.Split("x");
            int dimx = Int32.Parse(dimr[0]);
            int dimy = Int32.Parse(dimr[1]);

            int numOfPrimas = dimx * dimy;

            if(numOfPrimas > 64)
            {
                await Context.Channel.SendMessageAsync("Too many primas, currently limiting at 64. (you tried to make " + numOfPrimas + ")");
                return;
            }

            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/";
            string tag = "-c";
            Size psize = new Size(_width, _height);
            Size mSize = getPrimSize(dim);

            Size newPrimaSize = getPrimSize(dim);
            List<Point> positions = getAllPositions(dim);
            Bitmap BaseBmp = new Bitmap(newPrimaSize.Width, newPrimaSize.Height);
            Graphics gr = Graphics.FromImage(BaseBmp);

            foreach (Point p in positions)
            {
                Bitmap prima = GetNewPrima(tag);

                using (Graphics graphics = Graphics.FromImage(prima))
                {
                    string primaName = getName();
                    PointF point = new PointF(2f, 280f);
                    Font lucFont = new Font("Lucida Console", 14);
                    SizeF size = graphics.MeasureString(primaName, lucFont);
                    RectangleF rect = new RectangleF(point, size);
                    rect.Height = rect.Height + 1;
                    graphics.FillRectangle(Brushes.Black, rect);
                    point.Y = point.Y + 2;
                    graphics.DrawString(primaName, lucFont, Brushes.White, point);
                    graphics.Dispose();
                }

                gr.DrawImage(prima, p);                
            }

            BaseBmp.Save(dppath + "/prima.png", ImageFormat.Png);

            await Context.Channel.SendFileAsync(dppath + "/prima.png", "ILLEGAL.");
        }

        private string getName()
        {
            try
            {
                WebClient w = new WebClient();
                byte[] dat = w.DownloadData("https://namey.muffinlabs.com/name.json?type=surname");
                string name = System.Text.Encoding.UTF8.GetString(dat);
                name = name.Substring(2, name.Length - 4);
                return name;
            }
            catch
            {
                return "netError";
            }
        }

        public List<Point> getAllPositions(string dim)
        {
            List <Point> points = new List<Point> { };

            string[] dimxy = dim.Split("x");
            int x = Int32.Parse(dimxy[0]);
            int y = Int32.Parse(dimxy[1]);

            for (int i = 0; i < x; i++)
            {
                for (int o = 0; o < y; o++)
                {
                    points.Add(new Point(_width * i, _height * o));
                }
            }

            return points;
        }

        public Size getPrimSize(string dim)
        {
            string[] dimxy = dim.Split("x");
            int basex = _width;
            int basey = _height;
            int x = Int32.Parse(dimxy[0]);
            int y = Int32.Parse(dimxy[1]);
            return new Size(basex * x, basey * y);
        }

        public Bitmap GetNewPrima(string tag)
        {
            Bitmap drawn = new Bitmap(_width, _height);
            Color baseprimary = Color.FromArgb(107, 112, 123);
            Color basesecondary = Color.FromArgb(217, 224, 226);
            Color basehighlight = Color.FromArgb(250, 178, 60);
            Color basefeet = Color.FromArgb(255, 242, 0);
            Color baselegs = Color.FromArgb(34, 177, 76);
            Color baseface = Color.FromArgb(78, 82, 93);
            Color baseweird = Color.FromArgb(78, 82, 93);

            Color normallegs = Color.FromArgb(240, 247, 249);
            Color normalface = Color.FromArgb(78, 82, 93);

            Random rnd = new Random();
            Bitmap primabitmap;           
            byte[] byteBuffer = Convert.FromBase64String(prima);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;
            primabitmap = (Bitmap)Bitmap.FromStream(memoryStream);
            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            Color PRIMARY = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Color SECONDARY = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Color HIGHLIGHT = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Color FEET = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Color LEGS = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Color FACE = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Color WEIRD = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

            SolidBrush pr = new SolidBrush(PRIMARY);
            SolidBrush se = new SolidBrush(SECONDARY);
            SolidBrush hl = new SolidBrush(HIGHLIGHT);
            SolidBrush fe = new SolidBrush(FEET);
            SolidBrush le = new SolidBrush(LEGS);
            SolidBrush fa = new SolidBrush(FACE);
            SolidBrush we = new SolidBrush(WEIRD);

            SolidBrush len = new SolidBrush(normallegs);
            SolidBrush fan = new SolidBrush(normalface);
            SolidBrush wen = new SolidBrush(normallegs);

            Graphics g = Graphics.FromImage(primabitmap);
            Graphics drawing = Graphics.FromImage(drawn);
            int y = primabitmap.Height;
            int x = primabitmap.Width;

            for (int i = 0; i < x; i++)
            {
                for (int o = 0; o < y; o++)
                {
                    Color p = primabitmap.GetPixel(i, o);
                    SolidBrush old = new SolidBrush(p);

                    if (p == baseprimary)
                    {
                        drawing.FillRectangle(pr, i, o, 1, 1);
                    }
                    else if (p == basesecondary)
                    {
                        drawing.FillRectangle(se, i, o, 1, 1);
                    }
                    else if (p == basehighlight)
                    {
                        drawing.FillRectangle(hl, i, o, 1, 1);
                    }
                    else if (p == basefeet)
                    {
                        drawing.FillRectangle(fe, i, o, 1, 1);
                    }
                    else if (p == baselegs)
                    {
                        drawing.FillRectangle(le, i, o, 1, 1);
                    }
                    else if (p == baseface)
                    {
                        drawing.FillRectangle(fan, i, o, 1, 1);
                    }
                    else if (p == baseweird)
                    {
                        drawing.FillRectangle(we, i, o, 1, 1);
                    }
                    else
                    {
                        drawing.FillRectangle(old, i, o, 1, 1);
                    }

                }
            }
            return drawn;
        }
    }
}
