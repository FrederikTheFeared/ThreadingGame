using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace IdleGame
{
    class GoldMine : GameObject
    {
        private Object mineLock = new Object();
        private int number;
        private int goldDeposit;
        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }

        public int GoldDeposit
        {
            get { return goldDeposit; }
            set { goldDeposit = value; }
        }

        public GoldMine(string imagePath, Vector2D startPosition, int number, int goldDeposit) : base(imagePath, startPosition)
        {
            this.Number = number;
            this.goldDeposit = goldDeposit;
            GameWorld.GoldmineAmount++;
            GameWorld.GoldMines.Add(this);
        }

        public override void StartThread()
        {
            
        }

        public int Mining(int amount)
        {
            lock (mineLock)
            {
                if (amount < goldDeposit)
                {
                    Thread.Sleep(5000);
                    goldDeposit = goldDeposit - amount;
                    return amount;
                }
                else if (goldDeposit > 0)
                {
                    Thread.Sleep(5000);
                    int maxAmount = goldDeposit;
                    goldDeposit = goldDeposit - maxAmount;
                    GameWorld.RemoveObjs.Add(this);
                    return maxAmount;
                }
                else
                {
                    Thread.Sleep(1000);
                    return 0;
                }
            }
        }
        public override void Update(float currentFPS)
        {
            if (goldDeposit == 0)
            {
                GameWorld.RemoveObjs.Add(this);
            }
            base.Update(currentFPS);
        }

        public override void Draw(Graphics dc)
        {
            dc.TranslateTransform(400, 300);
            float angle = (360 / GameWorld.GoldmineAmount)*Number;
            dc.RotateTransform(angle);
            dc.TranslateTransform(position.X, position.Y);
            dc.RotateTransform(-angle);
            dc.DrawImage(sprite, 0 - sprite.Width / 2, 0 - sprite.Height / 2, sprite.Width, sprite.Height);
            dc.DrawRectangle(new Pen(Brushes.Red), 0 - sprite.Width / 2, 0 - sprite.Height / 2, CollisionBox.Width, CollisionBox.Height);
            dc.ResetTransform();
        }

    }
}
