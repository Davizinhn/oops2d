using oops2d.Core.Internal;
using Raylib_cs;
using System.Diagnostics;

namespace oops2d.Core
{
    public class Game
    {
        public static Game Instance { get; private set; }

        public static Window window { get; private set; } = new Window(1280, 720, "Window");
        public static Scene2D CurrentScene;
        

        public Game(Window desiredWindow, Scene2D initialScene)
        {
            if (Instance != null)
            {
                Debug.Fail("An oops2d Game instance is already running!");
                return;
            }

            window = desiredWindow != null ? desiredWindow : window;

            if (initialScene == null)
            {
                Debug.Fail("oops2d Game Missing the initial Scene2D");
                return;
            }

            Initialize(initialScene);
        }

        void Initialize(Scene2D initialScene)
        {
            Raylib.InitWindow(window.Width, window.Height, window.Name);

            Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(0));

            Raylib.InitAudioDevice();
            new Cache();
            ChangeScene(initialScene);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                CurrentScene.Update();

                Raylib.BeginMode2D(CurrentScene.camera2D);
                CurrentScene.Draw();
                Raylib.EndMode2D();

                CurrentScene.DrawUI();

                Raylib.EndDrawing();
            }

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
        }

        public static void ChangeScene(Scene2D newScene)
        {
            if (CurrentScene == newScene) return;

            Scene2D oldScene = CurrentScene;
            if (oldScene != null)
            {
                oldScene.Destroy();
            }
            CurrentScene = newScene;
            CurrentScene.Start();
        }
    }
}
