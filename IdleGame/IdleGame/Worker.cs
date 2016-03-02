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
        public Worker(string imagePath, Vector2D startPosition, Graphics dc) : base (imagePath, startPosition, dc)
        {
            GameWorld.Threads.Add(new Thread(() => Update(GameWorld.CurrentFPS))); 
        }

        private void Update(int fps)
        {
            
            GameWorld.FinishedThreads++;
        }
    }
}
