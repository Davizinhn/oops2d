using System.Numerics;

namespace oops2d.utils
{
    public static class Math
    {
        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static Vector2 NormalizeVector(Vector2 vector)
        {
            float clampValue = vector.X != 0 && vector.Y != 0 ? (float)0.75 : 1;
            return new Vector2(Clamp(vector.X, -clampValue, clampValue), Clamp(vector.Y, -clampValue, clampValue));
        }

        public static float Lerp(float start, float end, float amount)
        {
            float result = start + amount * (end - start);

            return result;
        }

        public static float FloatDistance(float f1, float f2)
        {
            return MathF.Abs(f1 - f2);
        }

    }
}
