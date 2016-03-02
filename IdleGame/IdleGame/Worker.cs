using System;
using System.Collections.Generic;
using System.Drawing;
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
        public override void Update(float currentFPS)
        {

            base.Update(currentFPS);
        }
        public override void Draw(Graphics dc)
        {
            base.Draw(dc);
        }
    }
}
