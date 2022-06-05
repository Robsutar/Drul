using Drul.Src.Game.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drull.WindowComponents
{
    static class Window
    {
        private static MainForm form;

        static Bitmap raze = new Bitmap(@"C:\Users\Robson\Pictures\raze fazendo dedinho.png");

        static int renderage = 0;
        static int up = 0, down = 0, left = 0, right = 0;

        public static Font Font => form.Font;

        public static void Initialize()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new MainForm();
            Application.Run(form);
            form.InitializeComponent();
        }

        public static void Construct()
        {
            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object sender, System.Timers.ElapsedEventArgs e)
            {
                form.Repaint();
            });
            myTimer.Interval = 8;
            myTimer.Start();
        }

        public static void keyDown(KeyEventArgs e)
        {
            //Console.WriteLine(e.KeyValue);
            if (e.KeyValue == 38)
            {
                up += 30;
            }else if (e.KeyValue == 39)
            {
                right += 30;
            }
            else if (e.KeyValue == 65)
            {
                left += 30;
            }
            else if (e.KeyValue == 83)
            {
                down += 30;
            }
        }

        public static void render(Graphics graphics)
        {
            renderage++;
            GraphicsGfx gfx = new GraphicsGfx(graphics);
            Point center = new Point(form.Width /2,form.Height/2) ;
            Point mouse = form.PointToClient(Cursor.Position);
            double angle = MouseIndicator.Angle(center, mouse);

            for (int i = 0; i < 16; i++)
            {
                Point pdist = MouseIndicator.AnglePoint(360/16f*i,600);
                pdist.X += center.X;
                pdist.Y += center.Y;
                gfx.DrawLine(center, pdist);
            }

            gfx.DrawLine(mouse.X - 600, mouse.Y, mouse.X + 600, mouse.Y);
            gfx.DrawLine(mouse.X, mouse.Y-600, mouse.X, mouse.Y+600);

            gfx.DrawLine(mouse, center);
            gfx.DrawString(MouseIndicator.GetDirection(angle) + " : " + angle, mouse.X, mouse.Y - 20);

            int size = 500;
            int margin = 10;

            up = ajeitar(up);
            down = ajeitar(down);
            left = ajeitar(left);
            right = ajeitar(right);

            //gfx.Translation = new Point(-200, 200);

            GraphicsPath p1 = new GraphicsPath();
            p1.AddArc(center.X-size/2, center.Y - size/2, size, size, 45-margin/2, -90+margin);
            GraphicsPath p2 = new GraphicsPath();
            p2.AddArc(center.X - size/2, center.Y - size/2, size, size, 135-margin/2, -90+margin);
            GraphicsPath p3 = new GraphicsPath();
            p3.AddArc(center.X-size/2, center.Y - size/2, size, size, 225-margin/2, -90+margin);
            GraphicsPath p4 = new GraphicsPath();
            p4.AddArc(center.X-size/2, center.Y - size/2 , size, size, 315-margin/2, -90+margin);

            for (int i = 0; i < 5; i++)
            {
                int s1 = size/5*i;
                gfx.DrawRect(center.X - s1 / 2, center.Y - s1 / 2, s1, s1);
            }

            gfx.PointAnchor = center;
            //gfx.Scale = (float)(((up + down + left + right) / 4 + 100) / 100f);
            gfx.Tickness = 20;

            gfx.Rotation = renderage;

            Console.WriteLine();
            
            gfx.Color = Color.Red;
            gfx.Scale = ((right/3+100)/100f);
            gfx.DrawPath(p1);
            
            gfx.Color = Color.Orange;
            gfx.Scale = ((down/3+100) / 100f);
            gfx.DrawPath(p2);
            
            gfx.Color = Color.Blue;
            gfx.Scale = ((left/3+100)/100f);
            gfx.DrawPath(p3);
            
            gfx.Color = Color.Lime;
            gfx.Scale = ((up/3+100)/100f);
            gfx.DrawPath(p4);
        }

        private static int ajeitar(int val)
        {
            return (int)Math.Max(0, val - val/10f);
        }
    }
}
