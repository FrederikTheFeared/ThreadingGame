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
        private int currentFPS;
        
        public static List<GameObject> Objs { get; set; }

        public GameWorld(Graphics dc, Rectangle displayRectangle)
        {
            Objs = objs;
            SetupWorld();
            this.displayRectangle = displayRectangle;
            this.backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);
            this.dc = backBuffer.Graphics;
        }

        public void SetupWorld()
        {

        }
        public void GameLoop()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan deltaTime = startTime - endTime;
            int milliSeconds = deltaTime.Milliseconds > 0 ? deltaTime.Milliseconds : 1;
            currentFPS = 1000 / milliSeconds;
            Update(currentFPS);
            Draw();
            endTime = DateTime.Now;
        }
        
        public void Update(float fps)
        {

        }

        public void Draw()
        {
            foreach (GameObject gameObject in Objs)
            {
                gameObject.Draw(dc);
            }
        }
    }
}