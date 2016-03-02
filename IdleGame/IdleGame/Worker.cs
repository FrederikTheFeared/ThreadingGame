using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace IdleGame
{
    class Worker : GameObject
    {
        private int goldCarry;
        private Vector2D target;
        public Worker(string imagePath, Vector2D startPosition) : base(imagePath, startPosition)
        {
            goldCarry = 50;
            GameWorld.FinishedThreads++;
        }

        public override void StartThread()
        {
            GameWorld.Threads.Add(new Thread(() => Update(GameWorld.CurrentFPS)));
            
        }

        private void Update(int fps)
        {
            //while (true)
            //{
            //    if (fps == 0)
            //    {
            //        fps = 1;
            //    }
            //    target = GameWorld.GoldMines[0].Position;
            //    //target = new Vector2D(0, 0);
            //    Vector2D velocity = this.position.Subtract(target);
            //    velocity.Normalize();
            //    position.X += (1 / fps) * (velocity.X);
            //    position.Y += (1 / fps) * (velocity.Y);
            //    Thread.Sleep(10);
            //}
            //GameWorld.FinishedThreads++;
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
            dc.DrawImage(sprite, position.X - sprite.Width / 2, position.Y - sprite.Height / 2, sprite.Width, sprite.Height);
            dc.DrawRectangle(new Pen(Brushes.Red), position.X - sprite.Width / 2, position.Y - sprite.Height / 2, CollisionBox.Width, CollisionBox.Height);
        }
    }
}
