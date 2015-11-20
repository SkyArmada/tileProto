using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace tileProto
{
    public static class handyFunctions
    {
        public static Vector2 radianToVector2(double radian)
        {
            Vector2 direction;

            direction.X = (float)Math.Sin(radian);
            direction.Y = -(float)Math.Cos(radian);

            return direction;
        }

        public static Vector2 pivotPointbyRadian(Vector2 point, double radian, int distanceX, int distanceY)
        {
            return new Vector2((point.X - (-(float)Math.Sin(radian) * distanceX)),(point.Y - ((float)Math.Cos(radian) * distanceY)));
        }
    }
}
