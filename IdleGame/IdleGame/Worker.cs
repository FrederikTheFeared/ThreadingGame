using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace IdleGame
{
    class Worker : GameObject
    {
        private int goldCarry;
        public Worker(string imagePath, Vector2D startPosition, Graphics dc) : base(imagePath, startPosition, dc)
        {
            goldCarry = 50;
            GameWorld.Threads.Add(new Thread(() => Update(GameWorld.CurrentFPS)));
        }

        private void Update(int fps)
        {

            GameWorld.FinishedThreads++;
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
