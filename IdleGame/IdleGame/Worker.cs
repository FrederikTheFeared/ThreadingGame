using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame
{
    class Worker : GameObject
    {
        private int goldCarry;
        public Worker(string imagePath, Vector2D startPosition) : base (imagePath, startPosition)
        {
            goldCarry = 50;
        }

        public override void OnCollision(GameObject other)
        {
            if(other is GoldMine)
            {
                (other as GoldMine).Mining(goldCarry);
            }
        }
    }
}
