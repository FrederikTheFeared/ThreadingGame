using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IdleGame
{
    class Background
    {
        private Image image;

        public Background(string image)
        {
            this.image = Image.FromFile(image);
        }

        public void Draw(Graphics dc)
        {
            dc.DrawImage(image, 0, 0, image.Width, image.Height);
        }
    }
}
