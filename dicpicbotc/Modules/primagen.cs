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
        public const string prima = "iVBORw0KGgoAAAANSUhEUgAAAQQAAAExCAMAAACtYSwmAAAABGdBTUEAALGPC/xhBQAAAwBQTFRFAAAAOzJ0TlJdYGVva3B7IrFM+rI8//IAqNrC2eDi8Pf58fj5AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAxapfrAAAAQB0Uk5T////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AFP3ByUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuMjHxIGmVAAAKEElEQVR4Xu2a7Y7jOBIEb293Z7/e/3nnQsx0HU1RsmS75V4g44dQLBbJqgCmp4GZ//wMPyMBIgEiASIBIgEiASIBIgEiAf4tEn654fVbiQSIBPgXSfi78RUeIgEiAb6nBCbthyWWAdFvvYVIgEiAj0hoIy54PZv6119/rQyB57/RF79OJEAkwKck/LdBoHH4auphqRg8fId230IkQCTAZyWINuUCg5eKIebr4TtI+sbXiASIBPgmEn78+PHPP/8QFJIAFRNocPFGD5EAkQCXSVDnFXv+G2TwoLlEKzclAdiqI1ry1bVPEwkQCXClhN9++601/suff/7JV7MUZAYPop1YkAcyqqdYp1TjZ54iEiAS4DIJQKt4KBWapYfk1IPQKQJXv89DJEAkwJdKaF0bLSVBsNQsPSR3PMByV5safOZlD5EAkQBfLcED334OgNcNlh7jBpl9CaLddKdCGT98kkiASIDLJBRDkqWmECynEshvUQU67rfPEAkQCfClEoCuPG3H0nqXJ5YB0BYeBhV9zYCOQC399mEiASIBPiJhTY0glpHuVUwLvFjBlp8/RiRAJMBXSwBa8qi7tLE2VQxbYpoUbPn5A0QCRAJ8HwmCYvAkDWXA6xVbW+TdwSMiASIBLpAA9OMh71lm28CTdEyTYqvezz8iEiAS4BoJQEue/EZ1zt+A+rWwh12hGujjNdNdkn5+l0iASIDLJAAtef5G3/byS+GuCuHqho6A1y94iASIBLhAQmt/wuABPNUNdu2iMRQLVXox80DGfWwTCRAJcI2E5UfABtPRgLwGF/seHDXWNWTcygaRAJEAF0gA2vDMM9gFd3xjyLA8KAHWZ93HBpEA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";

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
            WebClient w = new WebClient();
            byte[] dat = w.DownloadData("https://namey.muffinlabs.com/name.json?type=surname");
            string name = System.Text.Encoding.UTF8.GetString(dat);
            name = name.Substring(2, name.Length - 4);
            return name;
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
                            //drawing.FillRectangle(fa, i, o, 1, 1);
                        }
                        else
                        {
                            //drawing.FillRectangle(fan, i, o, 1, 1);
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
