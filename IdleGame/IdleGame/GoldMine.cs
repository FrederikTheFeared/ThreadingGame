using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IdleGame
{
    class GoldMine : GameObject
    {
        public GoldMine(string imagePath, Vector2D startPosition, Graphics dc) : base(imagePath, startPosition, dc)
        {

        }
    }
}
