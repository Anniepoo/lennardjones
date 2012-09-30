using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace lennardjones
{
    public partial class ljmainform : Form
    {
        PointF[] dots = new PointF[NUMDOTS];
        Brush[] dotcolors = new Brush[NUMDOTS];
        bool[] dotselected = new bool[NUMDOTS];
        PointF[] dotvel = new PointF[NUMDOTS];

        Random rand = new Random();

        PointF offset = new PointF(0.0f , 0.0f);

        int frameNum = 0;

        const int DOTSIZE = 7;
        const int BOXSIZE = 2;
        const int NUMDOTS = 50;
        const float RADIUS_OFFSET = 3.0f;
        const float LEADER_EFFECT = 25.0f;
        const float LEADER_DRIFT = 0.01f;
        const float MASS = 500.0f;
        const float FRIC_COEFFICIENT = 0.998f;

        bool runThread = true;

        const float A = 1.0f;   // was 1
        const float B = 45.0f;   // was 45 make 75 for 45 manhatten dist or newton

        public ljmainform()
        {
            InitializeComponent();
            this.SetStyle(
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint |
              ControlStyles.DoubleBuffer, true);
            this.WindowState = FormWindowState.Maximized;

            frameNum = 0;
            this.BackColor = Color.White;

            for(int i = 0 ; i < NUMDOTS ; i++)
            {
                dots[i] = new PointF(rand.Next(this.Left ,this.Right) , rand.Next(this.Top, this.Bottom));
                dotcolors[i] = new SolidBrush(Color.FromArgb(128, rand.Next(255) , rand.Next(255) , rand.Next(255)));
                dotselected[i] = false;
                dotvel[i] = new PointF(0.0f, 0.0f); // only used if newtonian is on
            }

            System.Threading.Thread t = new Thread(new ThreadStart(doThread));
            t.Start();
        }

        public void doThread()
        {
            while (runThread)
            {
                frameNum++;
                moveDots();

                if (frameNum % 6 == 0) // update display at 15 fps
                    this.Invalidate();
             //   Console.Out.WriteLine("========= frame  =========");
                Thread.Sleep(10);
            }
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            runThread = false;
            base.OnClosing(e);
        }

        private void moveDots()
        {
            for (int i = 0; i < NUMDOTS; i++)
            {
                float dx = 0.0f;
                float dy = 0.0f;

                for (int j = 0; j < NUMDOTS; j++)
                {
                    if (dots[i].X != dots[j].X || dots[i].Y != dots[j].Y)
                    {
                        float dist;
                        float BtoUse = B;
                        if (this.radioCart.Checked)
                        {
                            dist = (float)Math.Sqrt((dots[i].X - dots[j].X) * (dots[i].X - dots[j].X) +
                                (dots[i].Y - dots[j].Y) * (dots[i].Y - dots[j].Y));
                        } else { // 45 deg manhatten
                            BtoUse = 75.0f;
                            dist = Math.Abs(dots[i].X+dots[i].Y - dots[j].X - dots[j].Y) + 
                                Math.Abs(dots[i].X - dots[i].Y +  dots[j].Y - dots[j].X);
                        }

                        // avoid the worst of the black hole well
                        dist += RADIUS_OFFSET;

                        float lj;

                        if (this.twoSides.Checked && j % 2 != i % 2)
                        {
                            BtoUse += 50.0f;
                            lj = A / dist / dist  - BtoUse / dist / dist / dist;
                        }
                        else
                        {
                            lj = A / dist / dist - BtoUse / dist / dist / dist;
                            if (dotselected[j]) lj *= LEADER_EFFECT;
                        }

                        dx += lj * (dots[i].X - dots[j].X) / dist;
                        dy += lj * (dots[i].Y - dots[j].Y) / dist;
                    }
                }

                float MOVE = 1000.0f;
                float MAXDIST = 15.0f;
                dx = Math.Max(-MAXDIST, Math.Min(MAXDIST, MOVE * dx));
                dy = Math.Max(-MAXDIST, Math.Min(MAXDIST, MOVE * dy));

                if (this.newtonian.Checked)
                {
                    dotvel[i].X -= dx / MASS;
                    dotvel[i].Y -= dy / MASS;
                    if (this.friction.Checked)
                    {
                        dotvel[i].X *= FRIC_COEFFICIENT;
                        dotvel[i].Y *= FRIC_COEFFICIENT;
                    }
                    dots[i].X += dotvel[i].X;
                    dots[i].Y += dotvel[i].Y;
                }
                else
                {
                    // negate because we got the vector direction wrong 180
                    dots[i].X -= dx;
                    dots[i].Y -= dy;
                }

                dots[i].X = Math.Max(0.0f, Math.Min((float)this.Width, dots[i].X));
                dots[i].Y = Math.Max(0.0f, Math.Min((float)this.Height, dots[i].Y));
                
                // make leaders follow mouse
                if (dotselected[i])
                {
                    dots[i].X = (1.0f - LEADER_DRIFT) * dots[i].X + LEADER_DRIFT * offset.X;
                    dots[i].Y = (1.0f - LEADER_DRIFT) * dots[i].Y + LEADER_DRIFT * offset.Y;
                }
                 
           /*     
            * hey -  interesting lesson - this was like 95% of the time consumed!
                Console.Out.Write(MOVE * dx);
                Console.Out.Write(" - ");
                Console.Out.Write(MOVE * dy);
                Console.Out.Write("  ");
                Console.Out.WriteLine(dots[i]);
            */
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            float RSQR = DOTSIZE * DOTSIZE;

            if (e.Button.Equals(MouseButtons.Left))
            {
                offset.X = e.X;
                offset.Y = e.Y;
            }
            else if (e.Button.Equals(MouseButtons.Right))
            {
                for (int i = 0; i < NUMDOTS; i++)
                {
                    if ((dots[i].X - e.X) * (dots[i].X - e.X) + (dots[i].Y - e.Y) * (dots[i].Y - e.Y) < RSQR)
                    {
                        dotselected[i] = !dotselected[i];
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            offset.X = e.X;
            offset.Y = e.Y;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

          //  Brush br = new SolidBrush(Color.FromArgb(rand.Next(255) , rand.Next(255) , rand.Next(255)));


            for(int i = 0; i < NUMDOTS ; i++)
            {
                e.Graphics.FillEllipse(dotcolors[i] ,(int)(dots[i].X) - DOTSIZE , (int)(dots[i].Y) - DOTSIZE ,
                    2 * DOTSIZE + 1, 2 * DOTSIZE + 1);
                if(dotselected[i])
                    e.Graphics.DrawEllipse(Pens.Black , (int)(dots[i].X) - DOTSIZE, (int)(dots[i].Y) - DOTSIZE,
                    2 * DOTSIZE + 1, 2 * DOTSIZE + 1);
                if (this.twoSides.Checked)
                    e.Graphics.FillRectangle((i % 2 == 0)?Brushes.AliceBlue : Brushes.Chocolate, (int)(dots[i].X) - BOXSIZE, (int)(dots[i].Y) - BOXSIZE,
                        2 * BOXSIZE + 1, 2 * BOXSIZE + 1);

            }
        } 
    }

}
