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
        public async Task Primagen()
        {
            string dppath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dicpicbot_data/";
            Random rnd = new Random();
            Bitmap primabitmap;
            Bitmap bmpReturn = null;
            const string prima = "iVBORw0KGgoAAAANSUhEUgAAAVYAAAFBCAMAAAAboneSAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAMAUExURQAAADsydE5SXWBlb2twe/qyPKjawtng4u/3+fD3+fH4+QAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALB2XFsAAAEAdFJOU////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wBT9wclAAAACXBIWXMAAA7DAAAOwwHHb6hkAAAAGXRFWHRTb2Z0d2FyZQBwYWludC5uZXQgNC4wLjIx8SBplQAAC3pJREFUeF7t2+uSrTaahOF22/vguv/73Z0ok6+FgHWoTQJl5/NjhRASiNcRFeOZ8X9+hUGyWiSrRbJaJKtFslokq0WyWiSrRbJaJKtFslokq0WyWiSrRbJaJKtFslp8+ax/zHR9D8lqkawW/4Ssfze3KpusFslq8cWyol2fD2M2pf7WtZLVIlkt7pW1RZvoeqvjn3/+WTMYqOisX3yhZLVIVovbZf1vgwED4Zcdh0uOQTk7vHutZLVIVoubZqXWbYKUFXcY41c5O5jUEy+SrBbJanH3rN++ffv4+MCgMCvUGAOmpDuUTVaLZLW4Pitb1FhFZ5hBWZaitlwqK+BWbeElfvnY8yWrRbJa3CLrX3/91VL88f37d/yyTsHMUJbajgnLYobrsZi7uEavOVeyWiSrxfVZAR+PshWXdXqY3CxL3IWBVt+gbLJaJKvFNVlbB+ElsxIuWaeHyQdlYXpW6wjac13ZZLVIVovLsirh/PcUdN3gUmFmmHmcldqTFnE5oxefJVktktXi+qxlmMQluxAuN7Nifk8t4Ha9+xTJapGsFtdkBXyn+nWmGN08xmwKvIWyQ9x+zYBboC71br9ktUhWi3tlXasoNEVaxt1coIsV3NLrzZLVIlktLssK+EjFe6iF2o073KLNScItvd4pWS2S1eILZCUsBrVpOAO6Xtm7hXmdwCZZLZLV4sqsgC9UtqWp1g616WxO0t56vd4mWS2S1eLirICPVMtZtcD//MR/lerhLnEN9OO1zbuY1Os9ktUiWS2uzwr4SBVt+hDTv0g9jEta3XAL6PqKsslqkawWV2ZtQTYMZUGdZrirus2wmLhSF1tl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";
            byte[] byteBuffer = Convert.FromBase64String(prima);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;
            primabitmap = (Bitmap)Bitmap.FromStream(memoryStream);
            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            Bitmap drawn = new Bitmap(342, 341);
            Color baseprimary = Color.FromArgb(107, 112, 123);
            Color basesecondary = Color.FromArgb(217, 224, 226);
            Color basehighlight = Color.FromArgb(250, 178, 60);

            Color PRIMARY = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Color SECONDARY = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Color HIGHLIGHT = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

            SolidBrush pr = new SolidBrush(PRIMARY);
            SolidBrush se = new SolidBrush(SECONDARY);
            SolidBrush hl = new SolidBrush(HIGHLIGHT);

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
                    else
                    {
                        drawing.FillRectangle(old, i, o, 1, 1);
                    }

                }
            }
            drawn.Save(dppath + "/prima.png", ImageFormat.Png);
            await Context.Channel.SendFileAsync(dppath + "/prima.png", "ILLEGAL.");
        }

    }
}
