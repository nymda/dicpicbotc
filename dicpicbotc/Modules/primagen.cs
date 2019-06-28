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
            const string prima = "iVBORw0KGgoAAAANSUhEUgAAAGQAAABkCAYAAABw4pVUAAAACXBIWXMAAAAAAAAAAQCEeRdzAAAHJUlEQVR4nO2cTY4kRQyF5wZchCWnYIHEEs7AhgULzsLFmFMAG5DYNMoRHnlePzvsSEc4uzNDsqayKisr6n3pv6jo+fDy8vLhsetY+wQee4Bc2ton8NgDJG0/fv/bi9gD5AJ2gPjrn38/2Woo7V/2Ssa8QMPYAaVdhNX2zdc/vYhFxP/zj9+/eJ4BWQmlXbAdQL779odPpsFo8UVcBCKvMSCroLQLthOIBiNiHwA0GHa8E0q7YF1A/v741WdBtQkQAaEf74DSLli1YW5gQBAKiusBwnOP61RCaRdwBRDMDVkoDA6GMoEj16mC0i7gSigioAUkAoXBERBix/uroLSLVyE+dtIYag6rgKLByHE1lHZBK4BgnGfx34OSATICw3qeWwOxqiQPygiIbi4t0+fKNW8JJAIFX0coIuoBhYHxwh1eAxvRB0jQLE9hYLxzI6BuB+QMlAiYUUFQCaVdyE4gUTBR0SugtAu5EwqWyGgzniBeNIJySyAeFC1WtKIaAREQ+vFZKO0C7oKCQlkVlVfqRoCchdIuXiUEz6zKSsBo033FqPTV74uEuFsBmS13UTD0kEw/chZKu5BdUDyxPEgsmWehXAYICyWrPiMDhuUKrJ4YEMwdFVC2QmBj1WYBK4/MdvBngGTC1nIQkbEaykyjiCGJ5ZBI2PJeZ1CWwTgGq2C0rQQSBfHLz78OPWWmymJAIouPS2Do8tGzVVA8GMfnCgiEgcd6rsfxKAxlw9dyIKNGik1oBRRrWeQwAWGZ502zQLYn9SiAnVBwflp0gSTH+vUzfQwDYlVt1lzbQFhAqqEwGEzkqJeMwGDHr0HIc95820DsgqLF9hYes0CyDaacO5rv1BesAuEBqYCi89ko0WdyCRYCIzDLfqBaBaMaCv62MQJyBooAsTr/7NzbYFi/OZyBgqsCEe9gQOTx6HwEctiWjXIrYFSGL1wVkMf632hOyFRd+vxtQHbCiEDRX9hanpkFomEwKFp4nTu2AamupKLXs4AQMO7r2YVFnT9Y5cWAoBe9CSAz14lA8UDgcx4IbBQtKLjswkLWWSiXhJH1lLM5hMFgorPEvwXIFWBEoVigqoAwKJbnXBrI6k5eD1meQG+pAjKypUCuBgOhoK0OWZ5nXB5I9RoXXhetquwd5RC96yQCpKxTnxVzNQh8Plr26uMZIAJDPpdBYTCYd2mNPVghINYdmrUzYW+0WUKDwHNGC4tWotbz1r9leEBGYY/pSYFoAZiYGbdjro1ffgSfeQNr+DwIHhBZgPQgYP5iXoK9ySgXsZtN6/uFiLNxLwIEO1oEhBPN7FqxtoPqXDPqzCMJXUKYLoOz1Ri7KSmQaotCYXAsENbulVF4PAOEeTD2J1koLCIsB+JB8cCosvEViJncJDAQSkZEBkREzEDxQuIWIDNQGIzZwkBD0I9n7mqd+L3KiRUIzDu2h6woFA1Hw2B/35cBgh4x24173jH6zgwQg6LftwWInqD1xREGekQWhrW7ZDQPz0P08ez3H1Wv24B4YASG91evkVyR+e3DEgjnx5K5eFzVzphWIAjm+FKYtGdgRCBkKi0vJ+gwWA2mDQiCsUT3YGioGe+YrbasguTdABlVTpa3iKEo7PEKGCvDVjsQLxSxigTvTslBleEqC+VdAInCiFRnlqdYfY4GMtNt6/e9i5CVgeGJgmHKClvWeTNAsPzVOexdA4kIZW318Y6lxNZN3mxP8n8f8XlUQLkUDG9JIgJHC86OcVlmplEUCGqupVAuBcN6T0Qo9iuhHB/XB8FegckCscabB2ItIcwscbCNDvqzLCBRKCMYFVDaYXjnR5OrBSMDJLL4+G6BWF7B3uOtM2W8YxS2IluBMmMWynYgMyul+hpW1YNbf47BcpUFRJ6/FZAzMPQ12PI1AjnEZ/+fFQj2SsQKGPAZ1wSy2lYAsUrb6Lg9EAxX1v/4psR6JSB4n3luZDxAIJl7QJjQWkBvn1dmZKG0C1llDIhnTGQUzyqlM+MBQkYFEHZ+ZNwaiAgAP62OxHLFOwvltkCYeCKIZxHhZqHcOqkz4TJjJB5bag964ANkZkSAsL5kxuM8axex0lZ21MZaHL3Wm1hcXG2wVpYaEe9gu2C8pZxbA8HtRBkoURje1qSzi6bvEoi1bcgDEQ1VEc+ogtIu5iognngZ4XA7UnQ/wAMEhKkMKwjF8pAKL2kXswpIJKycBRK1M1DaxawC4iXcCqFG4dBYJ7snEITimZyb9ZpRtaWPZdy6U4+GFetOz3zGMZzrfDGyobJdyCsAiUKJ/N6iocyUxO1C7oYiQjFAEdE8oFZeyUBpF7HSvDsYB+svZoGIV7BGMduntIu4Aog3rO76bILXn215x+2AIBS9fMJAnO1N9PtxGX72c9oFXA2EhY6qrjoKLfOedgFXQdF3qQVlNZAZa5/AarOSd9Vi4ANkAgjr0rvnZdl/UnLOB7qS7KAAAAAASUVORK5CYII=";
            byte[] byteBuffer = Convert.FromBase64String(prima);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;
            primabitmap = (Bitmap)Bitmap.FromStream(memoryStream);
            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            Bitmap drawn = new Bitmap(100, 100);
            Color baseprimary = Color.FromArgb(117, 113, 121);
            Color basesecondary = Color.FromArgb(240, 239, 216);
            Color basehighlight = Color.FromArgb(246, 217, 15);

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
            drawn = new Bitmap(drawn, 500, 500);
            drawn.Save(dppath + "/prima.png", ImageFormat.Png);
            await Context.Channel.SendFileAsync(dppath + "/prima.png", "ILLEGAL.");
        }

    }
}
