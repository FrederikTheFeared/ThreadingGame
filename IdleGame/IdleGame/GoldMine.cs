using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame
{
    class GoldMine : GameObject
    {
        public GoldMine(string imagePath, Vector2D startPosition) : base(imagePath, startPosition)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }
    }
}
