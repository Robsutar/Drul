using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drull.WindowComponents
{
    public class GraphicsGfx
    {
        readonly Graphics graphics;

        float _rotation = 0;
        float _scale = 1;

        ImageAttributes imgAttribute; 
        ColorMatrix colormatrix;

        public SolidBrush Brush;
        public int Tickness{get;set;}
        public Pen Pen => new Pen(Brush,Tickness);
        public Color Color
        {
            get => Brush.Color;
            set{
                Brush.Color = Color.FromArgb((int)(Opacity * 255), value.R, value.G, value.B);
            }
        }
        public float Opacity
        {
            set{
                float val = Math.Max(Math.Min(value,1f),0f);
                Brush.Color =  Color.FromArgb((int)(val * 255), Brush.Color.R, Brush.Color.G, Brush.Color.B);
                colormatrix.Matrix33 = val;
                imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            }
            get => Color.A/255f;
        }
        public float Rotation
        {
            get => _rotation;
            set
            {
                Matrix m = graphics.Transform;
                m.RotateAt(_rotation, PointAnchor, MatrixOrder.Append);
                graphics.Transform = m;
                _rotation = value;
                m = graphics.Transform;
                m.RotateAt(_rotation, PointAnchor, MatrixOrder.Append);
                graphics.Transform = m;
            }
        }
        public float Scale
        {
            get => _scale;
            set
            {
                float exit = value - _scale;
                _scale = exit+1;
                Matrix m = graphics.Transform;
                m.Scale(_scale, _scale, MatrixOrder.Append);
                m.Translate(PointAnchor.X, PointAnchor.Y, MatrixOrder.Append);
                m.Translate(-PointAnchor.X*_scale, -PointAnchor.Y * _scale, MatrixOrder.Append);
                _scale = value;

                graphics.Transform = m;
            }
        }
        public Point Translation
        {
            set
            {
                Matrix m = graphics.Transform;
                m.Translate(value.X,value.Y, MatrixOrder.Append);
                graphics.Transform = m;
            }
        }
        public Point PointAnchor { get; set; }
        public GraphicsGfx(Graphics graphics)
        {
            this.graphics = graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Brush = new SolidBrush(Color.White);
            colormatrix = new ColorMatrix();
            imgAttribute = new ImageAttributes();

            Opacity = 1f;
            Scale = 1f;
            PointAnchor = new Point(0, 0);
            Rotation = 0f;
            Tickness = 1;
        }

        //Raw Methods
        public void DrawImage(Bitmap img,int x,int y)
        {
            graphics.TranslateTransform(x, y);
            graphics.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.TranslateTransform(-x, -y);
        }

        public void FillRect(int x,int y,int width,int height)
        {
            graphics.FillRectangle(Brush, x, y, width, height);
        }
        public void DrawRect(int x, int y, int width, int height)
        {
            graphics.DrawRectangle(Pen, x, y, width, height);
        }

        public void DrawPath(GraphicsPath path)
        {
            graphics.DrawPath(Pen, path);
        }

        public void DrawLine(int x,int y,int x1, int x2)
        {
            graphics.DrawLine(Pen,x, y, x1, x2);
        }
        public void DrawString(string str,int x, int y)
        {
            graphics.DrawString(str,Window.Font, Brush, x, y);
        }

        //Overload
        public void DrawLine(Point p1, Point p2) { DrawLine(p1.X, p1.Y, p2.X, p2.Y); }
    }
}
