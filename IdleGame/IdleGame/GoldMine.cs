using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public GoldMine(string imagePath, Vector2D startPosition, int number, int goldDeposit) : base(imagePath, startPosition)
        {
            this.Number = number;
            this.goldDeposit = goldDeposit;
        }

        public override void OnCollision(GameObject other)
        {

        }

        public int Mining(int amount)
        {
            if(amount <= goldDeposit)
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
            if(goldDeposit == 0)
            {
                GameWorld.RemoveObjs.Add(this);
            }
            base.Update(currentFPS);
        }

        public override void Draw(Graphics dc)
        {
            dc.TranslateTransform(400, 300);
            
            
        }

    }
}
