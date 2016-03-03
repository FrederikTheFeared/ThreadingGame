using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace IdleGame
{
    class Bank : GameObject
    {
        private Semaphore bankSemaphore = new Semaphore(0, 5);
        private int gold;
        private Vector2D startPosition;
        public Bank(string imagePath, Vector2D startPosition) : base(imagePath, startPosition)
        {
            this.startPosition = startPosition;
            bankSemaphore.Release(5);
        }
        public void Deposit(int gold)
        {
            bankSemaphore.WaitOne();
            Thread.Sleep(1000);
            this.gold += gold;
            if (this.gold >= 250)
            {
                GameWorld.AddObjs.Add(new Worker("testPlayer.png", new Vector2D(0, 0), 50));
                this.gold -= 250;
            }
            bankSemaphore.Release();
        }
        public override void StartThread()
        {
            
        }
        public override void Update(float currentFPS)
        {
            base.Update(currentFPS);
        }
        public override void Draw(Graphics dc)
        {
            dc.TranslateTransform(startPosition.X, startPosition.Y);
            dc.DrawImage(sprite, 0-sprite.Width/2, 0-sprite.Height/2, sprite.Width, sprite.Height);
            dc.DrawRectangle(new Pen(Brushes.Red), 0 - sprite.Width / 2, 0 - sprite.Height / 2, CollisionBox.Width, CollisionBox.Height);
            dc.ResetTransform();
        }
    }
}
