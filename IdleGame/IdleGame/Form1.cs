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
                gw = new IdleGame.GameWorld(dc, this.DisplayRectangle);
            }
            gw.GameLoop();
        }
    }
}
