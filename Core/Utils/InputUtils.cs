using System.Numerics;
using Raylib_cs;

namespace oops2d.Core.Utils
{
    public static class Input
    {
        public static Vector2 GetDirectionalInput()
        {
            Vector2 wasd = new Vector2(Raylib.IsKeyDown(KeyboardKey.A) ? -1 : Raylib.IsKeyDown(KeyboardKey.D) ? 1 : 0, Raylib.IsKeyDown(KeyboardKey.W) ? -1 : Raylib.IsKeyDown(KeyboardKey.S) ? 1 : 0);
            Vector2 arrows = new Vector2(Raylib.IsKeyDown(KeyboardKey.Left) ? -1 : Raylib.IsKeyDown(KeyboardKey.Right) ? 1 : 0, Raylib.IsKeyDown(KeyboardKey.Up) ? -1 : Raylib.IsKeyDown(KeyboardKey.Down) ? 1 : 0);

            return new Vector2(Math.Clamp(wasd.X+arrows.X, -1, 1), Math.Clamp(wasd.Y+arrows.Y, -1, 1));
        }
    }
}
