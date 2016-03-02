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
        private GameObject target;
        private GameObject targetBank;
        private bool targetingBank = false;
        private int rotationNumber;
        private int currentlyCarrying = 0;
        public Worker(string imagePath, Vector2D startPosition) : base(imagePath, startPosition)
        {
            goldCarry = 50;
            GameWorld.FinishedThreads++;
            Thread workerThread = new Thread(() => Update(GameWorld.CurrentFPS));
            GameWorld.Threads.Add(workerThread);
            target = GameWorld.GoldMines[0];
            foreach (GameObject gameObject in GameWorld.Objs)
            {
                if (gameObject is Bank)
                {
                    targetBank = gameObject;
                }
            }
        }

        public override void StartThread()
        {
            
        }

        private void Update(int fps)
        {
            while (position != target.Position)
            {
                if(target == null)
                {
                    target = GameWorld.GoldMines[0];
                }
                if(target is GoldMine)
                {
                    GoldMine newTarget = (GoldMine)target;
                    rotationNumber = newTarget.Number;
                }
                float distance = (float)Math.Sqrt((target.Position.X - Position.X) * (target.Position.X - Position.X) + (target.Position.Y - Position.Y) * (target.Position.Y - Position.Y));
                if(distance < 1)
                {
                    if(targetingBank == false)
                    {
                        GoldMine newTarget = (GoldMine)target;
                        currentlyCarrying = newTarget.Mining(goldCarry);
                        foreach (GameObject gameObject in GameWorld.Objs)
                        {
                            if(gameObject is Bank)
                            {
                                target = gameObject;
                                targetingBank = true;
                            }
                        }
                    }
                    else
                    {
                        target = GameWorld.GoldMines[0];
                        GameWorld.Gold += currentlyCarrying;
                        currentlyCarrying = 0;
                        targetingBank = false;
                    }

                }
                //target = new Vector2D(0, 0);
                Vector2D velocity = this.position.Subtract(target.Position);
                velocity.Normalize();
                    position.X += (1 / (GameWorld.CurrentFPS + 1) + 1) * (velocity.X);
                    position.Y += (1 / (GameWorld.CurrentFPS + 1) + 1) * (velocity.Y);
                Thread.Sleep(10);
            }
            GameWorld.FinishedThreads++;
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
            dc.TranslateTransform(400, 300);
            dc.RotateTransform((360 / GameWorld.GoldmineAmount) * rotationNumber);
            dc.DrawImage(sprite, position.X - sprite.Width / 2, position.Y - sprite.Height / 2, sprite.Width, sprite.Height);
            dc.DrawRectangle(new Pen(Brushes.Red), position.X - sprite.Width / 2, position.Y - sprite.Height / 2, CollisionBox.Width, CollisionBox.Height);
            dc.ResetTransform();
        }
    }
}
