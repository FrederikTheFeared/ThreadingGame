using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IdleGame
{
    class GoldMine : GameObject
    {
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

        public GoldMine(string imagePath, Vector2D startPosition, Graphics dc) : base(imagePath, startPosition, dc)
        {
            this.Number = number;
            this.goldDeposit = goldDeposit;
        }

        public override void OnCollision(GameObject other)
        {

        }

        public int Mining(int amount)
        {
            if (amount <= goldDeposit)
            {
                goldDeposit = goldDeposit - amount;
                return amount;
            }
            else
            {
                int maxAmount = goldDeposit;
                goldDeposit = goldDeposit - maxAmount;
                return maxAmount;
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
            float angle = 360 / number;
            dc.RotateTransform(angle);
            dc.DrawImage(sprite, position.X, position.Y, sprite.Width, sprite.Height);
            dc.DrawRectangle(new Pen(Brushes.Red), CollisionBox.X, CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);
            dc.ResetTransform();


        }

    }
}
