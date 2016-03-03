using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
namespace IdleGame
{
    class GameWorld
    {
        private static List<GameObject> objs = new List<GameObject>();
        private static List<Thread> threads = new List<Thread>();
        private static int finishedThreads = 0; 
        private static List<GameObject> removeObjs = new List<GameObject>();
        private static List<GameObject> addObjs = new List<GameObject>();
        private static List<GoldMine> goldMines = new List<GoldMine>();
        private static List<Worker> workers = new List<Worker>();
        private Graphics dc;
        private Rectangle displayRectangle;
        private BufferedGraphics backBuffer;
        private DateTime endTime;
        private TimeSpan deltaTime2;
        private static int currentFPS;
        static public int GoldmineAmount = 0;
        private static int gold = 0;

        public static string debug = "debug";

        public static List<Thread> Threads
        {
            get { return threads; }
            set { threads = value; }
        }

        public static List<GameObject> Objs
        {
            get
            {
                return objs;
            }

            set
            {
                objs = value;
            }
        }

        internal static List<GameObject> RemoveObjs
        {
            get
            {
                return removeObjs;
            }

            set
            {
                removeObjs = value;
            }
        }

        internal static List<GameObject> AddObjs
        {
            get { return addObjs; }
            set { addObjs = value; }
        }

        internal static List<GoldMine> GoldMines
        {
            get
            {
                return goldMines;
            }

            set
            {
                goldMines = value;
            }
        }

        internal static List<Worker> Workers
        {
            get { return workers; }
            set { workers = value; }
        }

        public GameWorld(Graphics dc, Rectangle displayRectangle)
        {
            Objs = Objs;
            this.displayRectangle = displayRectangle;
            this.backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);
            this.dc = backBuffer.Graphics;
            deltaTime2 = DateTime.Now - DateTime.Now;
            SetupWorld();
        }

        public static int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        public static int CurrentFPS
        {
            get { return currentFPS; }
        }

        public static int FinishedThreads
        {
            get { return finishedThreads; }
            set { finishedThreads = value; }
        }

        public void SetupWorld()
        {
            Objs.Add(new Bank("testExplosion.png", new Vector2D(0,0)));
            Objs.Add(new GoldMine("Mine.png", new Vector2D(-200, 0), 1, 500));
            Objs.Add(new GoldMine("Mine.png", new Vector2D(-200, 0), 2, 500));
            Objs.Add(new GoldMine("Mine.png", new Vector2D(-200, 0), 3, 500));
            Objs.Add(new GoldMine("Mine.png", new Vector2D(-200, 0), 4, 500));
            Objs.Add(new Worker("testPlayer.png", new Vector2D(0, 0)));
        }

        public void GoldMineNumberReset(GoldMine removed)
        {
            foreach (GoldMine goldMine in goldMines)
            {
                if(goldMine != removed)
                {
                    if (goldMine.Number > removed.Number)
        {
                        goldMine.Number = goldMine.Number - 1;
                    }
                }
            }
        }
        public void GameLoop()
        {
            int j = RemoveObjs.Count;
            for (int i = 0; i < j; i++)
            {
                if(RemoveObjs[i] is GoldMine)
                {
                    GoldMineNumberReset(RemoveObjs[i] as GoldMine);
                    GoldmineAmount--;
                }
                Objs.Remove(RemoveObjs[i]);
            }
            RemoveObjs.Clear();
            int k = AddObjs.Count;
            for (int i = 0; i < k; i++)
            {
                Objs.Add(AddObjs[i]);
            }
            AddObjs.Clear();
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
            if (FinishedThreads >= threads.Count)
            {
                foreach (GameObject obj in objs)
                {
                    obj.StartThread();
                }
                foreach (Thread thread in threads)
                {
                    thread.Start();
                }
            }
            threads.Clear();
            goldMines[0].GoldDeposit = 5000000;
        }

        public void Draw()
        {
            dc.Clear(Color.Beige);

            foreach (GameObject gameObject in Objs)
            {
                gameObject.Draw(dc);
            }
            Font f = new Font("Arial", 16);
            dc.DrawString("Gold: " + gold, f, Brushes.Black, 100,5);
#if DEBUG
            dc.DrawString(" " + currentFPS, f, Brushes.Black, 0, 0);
            dc.DrawString(debug, f, Brushes.Black, 0, 20);
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