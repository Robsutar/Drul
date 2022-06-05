using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drul.Src.Game.Helpers
{
    static class MouseIndicator
    {
        public static float Angle(Point center,Point mouse)
        {
            float xDiff = mouse.X - center.X;
            float yDiff = mouse.Y - center.Y;
            float raw = (float)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);

            if (raw > 0)
            {
                raw = 360-raw;
            }else
            {
                raw *= -1;
            }

            return raw;
        }

        public static Point AnglePoint(float angle,float radius)
        {
            var x = radius * Math.Sin(Math.PI * 2 * angle / 360);
            var y = radius * Math.Cos(Math.PI * 2 * angle / 360);
            return new Point((int)x, (int)y);
        }

        public static Direction GetDirection(double angle)
        {
            if (angle >= 45 && angle < 135)
            {
                return Direction.Up;
            }else if (angle >= 135 && angle < 225)
            {
                return Direction.Left;
            }
            else if (angle >= 225 && angle < 315)
            {
                return Direction.Down;
            }
            return Direction.Right;
        }
    }
    public enum Direction
    {
        Up,Down,Left,Right
    }
}
