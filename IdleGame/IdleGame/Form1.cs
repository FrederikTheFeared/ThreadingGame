using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleGame
{
    public partial class Form1 : Form
    {
        Graphics dc;
        IdleGame.GameWorld gw;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(dc == null)
            {
                dc = CreateGraphics();
            }
            gw = new GameWorld(dc, this.DisplayRectangle);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gw == null)
            {
                dc = CreateGraphics();
                gw = new IdleGame.GameWorld(dc, this.DisplayRectangle);
            }
            gw.GameLoop();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int number = 1;
            foreach ( GoldMine goldmine in GameWorld.GoldMines)
            {
                number++;
            }
            GameWorld.Objs.Add(new GoldMine("mine.png", new Vector2D(-200, 0), number, 500));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            GameWorld.Objs.Add(new Worker("testPlayer.png", new Vector2D(0,0), 50));
        }
    }
}
