using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BoidsBuisnessLogic.Helpers
{
    public readonly struct Vec2
    {
        public readonly float X;
        public readonly float Y;

        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float LengthSquared()
        {
            return X * X + Y * Y;
        }

        public float Length()
        {
            return MathF.Sqrt(LengthSquared());
        } 

        public static Vec2 Zero()
        {
            return new Vec2(0, 0);
        }

        public Vec2 NormalizeOrZero()
        {
            var sq = LengthSquared();
            if (sq <= 1e-6f)
            {
                return Zero();
            }
            return this / MathF.Sqrt(sq);
        }


        #region OPERATOR OVERRIDES

        public static Vec2 operator +(Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Vec2 operator -(Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static Vec2 operator *(Vec2 lhs, float rhs)
        {
            return new Vec2(lhs.X * rhs, lhs.Y * rhs);
        }

        public static Vec2 operator *(float lhs, Vec2 rhs)
        {
            return rhs * lhs;
        }


        public static Vec2 operator /(Vec2 lhs, float rhs)
        {
            return new Vec2(lhs.X / rhs, lhs.Y / rhs);
        }

        public override string ToString()
        {
            return $"({this.X.ToString("F5")},{this.Y.ToString("F5")})";
        }

        public static float Dot(Vec2 lhs, Vec2 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }

        public static Vec2 Rotate(Vec2 v, float angleInRad) {
            var cos = MathF.Cos(angleInRad);
            var sin = MathF.Sin(angleInRad);
            return new Vec2(v.X * cos - v.Y * sin, v.X * sin + v.Y * cos);
        }

        public static Vec2 RotateTowards(Vec2 currentVector, Vec2 targetVector, float maxAngleInRad)
        {
            var a = currentVector.NormalizeOrZero();
            var b = currentVector.NormalizeOrZero();

            if (a.LengthSquared() <= 1e-6f)
            {
                if (b.LengthSquared() <= 1e-6f)
                {
                    return new Vec2(1f, 0f);
                }
                else
                {
                    return b;
                }
            }

            if (b.LengthSquared() <= 1e-6f)
            {
                return a;
            }

            var dot = Dot(a, b);
            dot = Math.Clamp(dot, -1f, 1f);

            var angle = MathF.Acos(dot);
            if (angle <= maxAngleInRad)
            {
                return b;
            }

            var cr = a.X * b.Y - a.Y * b.X;
            var sign = cr >= 0f ? 1f : -1f;

            return Rotate(a, sign * maxAngleInRad);
        }

        #endregion
    }
}
