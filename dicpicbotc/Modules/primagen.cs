using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dicpicbotc.Modules
{
    public class primagen : ModuleBase<SocketCommandContext>
    {
        [Command("prima")]
        public async Task Primagen(string dim = "1x1")
        {
            //if (!(dim.Contains("x"))){
            //    double primasd = Math.Sqrt(double.Parse(dim));
            //    int primas = (int)Math.Ceiling(primasd);
            //    Console.WriteLine(primas);
            //}

            if(dim != "1x1")
            {
                await Context.Channel.SendMessageAsync("Hold on, processing...");
            }

            String[] dimr = dim.Split("x");
            int dimx = Int32.Parse(dimr[0]);
            int dimy = Int32.Parse(dimr[1]);

            int numOfPrimas = dimx * dimy;

            if(numOfPrimas > 100)
            {
                await Context.Channel.SendMessageAsync("Too many primas, limiting at 100. (you tried to make " + numOfPrimas + ")");
                return;
            }

            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/";
            string tag = "-c";
            Size psize = new Size(342, 341);
            Size mSize = getPrimSize(dim);

            Size newPrimaSize = getPrimSize(dim);
            List<Point> positions = getAllPositions(dim);
            Bitmap BaseBmp = new Bitmap(newPrimaSize.Width, newPrimaSize.Height);
            Graphics gr = Graphics.FromImage(BaseBmp);

            foreach (Point p in positions)
            {
                Bitmap prima = GetNewPrima(tag);
                gr.DrawImage(prima, p);                
            }

            BaseBmp.Save(dppath + "/prima.png", ImageFormat.Png);

            await Context.Channel.SendFileAsync(dppath + "/prima.png", "ILLEGAL.");
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
                    points.Add(new Point(342 * i, 341 * o));
                }
            }

            return points;
        }

        public Size getPrimSize(string dim)
        {
            string[] dimxy = dim.Split("x");
            int basex = 342;
            int basey = 341;
            int x = Int32.Parse(dimxy[0]);
            int y = Int32.Parse(dimxy[1]);
            return new Size(basex * x, basey * y);
        }

        public Bitmap GetNewPrima(string tag)
        {
            Bitmap drawn = new Bitmap(342, 341);
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
            const string prima = "iVBORw0KGgoAAAANSUhEUgAAAVYAAAFBCAMAAAAboneSAAAABGdBTUEAALGPC/xhBQAAAwBQTFRFAAAAOzJ0TlJdYGVva3B7IrFM+rI8//IAqNrC2eDi8Pf58fj5AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAxapfrAAAAQB0Uk5T////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AFP3ByUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuMjHxIGmVAAALWUlEQVR4Xu3b67KdNhqE4ck4PiS5/+v1NOrmGyFgHbZpYDv9/FglhATidZVrXDX5z88wSFaLZLVIVotktUhWi2S1SFaLZLVIVotktUhWi2S1SFaLZLVIVotktUhWi0+f9Y+Zru8hWS2S1eJ3yPp3c6uyyWqRrBafLCva9fkwZlPqb10rWS2S1eJeWVu0ia63On758qVmMFDRWb/4QslqkawWt8v63wYDBsIvOw6XHINydnj3WslqkawWN81KrdsEKSvuMMavcnYwqSdeJFktktXi7lm/ffv2zz//YFCYFWqMAVPSHcomq0WyWlyflS1qrKIzzKAsS1FbLpUVcKu28BK/fOz5ktUiWS1ukfXPP/9sKf748eMHflmnYGYoS23HhGUxw/VYzF1co9ecK1ktktXi+qyAj0fZiss6PUxuliXuwkCrb1A2WS2S1eKarK2D8JJZCZes08Pkg7IwPat1BO25rmyyWiSrxWVZlXD++xR03eBSYWaYeZyV2pMWcTmjF58lWS2S1eL6rGWYxCW7EC43s2J+Ty3gdr37FMlqkawW12QFfKf6daYY3TzGbAq8hbJD3H7NgFugLvVuv2S1SFaLe2Vdqyg0RVrG3VygixXc0uvNktUiWS0uywr4SMV7qIXajTvcos1Jwi293ilZLZLV4hNkJSwGtWk4A7pe2buFeZ3AJlktktXiyqyAL1S2panWDrXpbE7S3nq93iZZLZLV4uKsgI9Uy1m1wP9+4j+lerhLXAP9eG3zLib1eo9ktUhWi+uzAj5SRZs+xPQPqYdxSasbbgFdX1E2WS2S1eLKrC3IhqEsqNMMd1W3GRYTV+piqyxmdA6DZLVIVouLs05/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";
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
                        if (tag == "-c")
                        {
                            drawing.FillRectangle(fe, i, o, 1, 1);
                        }
                        else
                        {
                            drawing.FillRectangle(len, i, o, 1, 1);
                        }
                    }
                    else if (p == baselegs)
                    {
                        if (tag == "-c")
                        {
                            drawing.FillRectangle(le, i, o, 1, 1);
                        }
                        else
                        {
                            drawing.FillRectangle(len, i, o, 1, 1);
                        }
                    }
                    else if (p == baseface)
                    {
                        if (tag == "-c")
                        {
                            drawing.FillRectangle(fa, i, o, 1, 1);
                        }
                        else
                        {
                            drawing.FillRectangle(fan, i, o, 1, 1);
                        }
                    }
                    else if (p == baseweird)
                    {
                        if (tag == "-c")
                        {
                            drawing.FillRectangle(we, i, o, 1, 1);
                        }
                        else
                        {
                            drawing.FillRectangle(wen, i, o, 1, 1);
                        }
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
