using System;
using System.Collections.Generic;
using System.Drawing;

namespace IdleGame
{
    class GameWorld
    {
        public static List<GameObject> objs = new List<GameObject>();
        private Graphics dc;
        private Rectangle displayRectangle;
        private BufferedGraphics backBuffer;
        private DateTime endTime;
        private TimeSpan deltaTime2;
        private int currentFPS;

        public static List<GameObject> Objs { get; set; }

        public GameWorld(Graphics dc, Rectangle displayRectangle)
        {
            Objs = objs;
            SetupWorld();
            this.displayRectangle = displayRectangle;
            this.backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);
            this.dc = backBuffer.Graphics;
            deltaTime2 = DateTime.Now - DateTime.Now;
            objs.Add(new Bank("testExplosion.png", new Vector2D(displayRectangle.Width / 2, displayRectangle.Height / 2), dc));
        }

        public void SetupWorld()
        {
            
        }
        public void GameLoop()
        {
            foreach (GameObject gameObject in Objs)
            {

            }
            DateTime startTime = DateTime.Now;
            TimeSpan deltaTime = startTime - endTime;
            deltaTime2 = deltaTime2 + deltaTime;
            endTime = DateTime.Now;
            if (deltaTime2.Milliseconds >= 100)
            {
                int milliSeconds = deltaTime.Milliseconds > 0 ? deltaTime.Milliseconds : 1;
                currentFPS = 1000 / milliSeconds;
                deltaTime2 = DateTime.Now - DateTime.Now;
            }
            Update(currentFPS);
            Draw();
        }

        public void Update(float fps)
        {

        }

        public void Draw()
        {
            dc.Clear(Color.Beige);

            foreach (GameObject gameObject in Objs)
            {
                gameObject.Draw(dc);
            }
#if DEBUG
            Font f = new Font("Arial", 16);
            dc.DrawString("" + currentFPS, f, Brushes.Black, 0, 0);
#endif
            try
            {
                backBuffer.Render();
            }
            catch (Exception)
            {
            }
        }
    }
}