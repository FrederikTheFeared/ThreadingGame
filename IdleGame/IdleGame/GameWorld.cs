using System;
using System.Drawing;

namespace IdleGame
{
    class GameWorld
    {
        private Graphics dc;
        private Rectangle displayRectangle;
        private BufferedGraphics backBuffer;
        private DateTime endTime;
        private int currentFPS;

        public GameWorld(Graphics dc, Rectangle displayRectangle)
        {
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