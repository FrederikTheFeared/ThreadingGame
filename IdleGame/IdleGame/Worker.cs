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
        private int gold;
        private GameObject target;
        private GameObject targetBank;
        private GameObject targetMine;
        private Thread workerThread;
        private Thread death;
        private bool targetingBank = false;
        private bool whileLoop = true;
        private int rotationNumber;
        private Random rnd = new Random();

        public Worker(string imagePath, Vector2D startPosition, int goldCarry) : base(imagePath, startPosition)
        {
            this.goldCarry = goldCarry;
            GameWorld.FinishedThreads++;
            workerThread = new Thread(() => Update(GameWorld.CurrentFPS));
            death = new Thread(Die);
            death.Start();
            GameWorld.Workers.Add(this);
            GameWorld.Threads.Add(workerThread);
            targetMine = (GameWorld.GoldMines[rnd.Next(GameWorld.GoldMines.Count)]);
            target = targetMine;
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
            while (whileLoop)
            {
                if (target == null)
                {
                    targetMine = (GameWorld.GoldMines[rnd.Next(GameWorld.GoldMines.Count)]);
                }
                if (target is GoldMine)
                {
                    GoldMine newTarget = (GoldMine)target;
                    rotationNumber = newTarget.Number;
                }
                float distance = (float)Math.Sqrt((target.Position.X - Position.X) * (target.Position.X - Position.X) + (target.Position.Y - Position.Y) * (target.Position.Y - Position.Y));
                if(distance < 5)
                {
                    if(targetingBank == false)
                    {
                        gold += (target as GoldMine).Mining(goldCarry);
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
                        (target as Bank).Deposit(gold);
                        gold = 0;
                        targetMine = (GameWorld.GoldMines[rnd.Next(GameWorld.GoldMines.Count)]);
                        target = targetMine;
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

        public void Die()
        {
            Thread.Sleep(100000);
            whileLoop = false;
            GameWorld.RemoveObjs.Add(this);
        }

        public override void Update(float currentFPS)
        {

            base.Update(currentFPS);
        }
        public override void Draw(Graphics dc)
        {
            float angle = (360 / GameWorld.GoldmineAmount) * rotationNumber;
            dc.TranslateTransform(400, 300);
            dc.RotateTransform(angle);
            dc.TranslateTransform(position.X, position.Y);
            dc.RotateTransform(-angle);
            dc.DrawImage(sprite, 0 - sprite.Width / 2, 0 - sprite.Height / 2, sprite.Width, sprite.Height);
            dc.DrawRectangle(new Pen(Brushes.Red), 0 - sprite.Width / 2, 0 - sprite.Height / 2, CollisionBox.Width, CollisionBox.Height);
            dc.ResetTransform();
        }
    }
}
