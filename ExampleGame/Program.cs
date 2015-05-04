using System.Threading;
using BubbasEngine.Engine;
using ExampleGame.GameContent;

namespace ExampleGame
{
    class Program
    {
        private static int _gamesRunning;

        static void Main(string[] args)
        {
            // GameConsole
            GameConsole.WindowWidth = 100;
            GameConsole.WindowHeight = 35;

            // Temporary testing values
            const float resScale = 1f;
            const uint winWidth = 1280u;
            const uint winHeight = 720u;

            //
            GameArgs gameArgs = new GameArgs()
            {
                Content = new BubbasEngine.Engine.Content.ContentManagerArgs() // Content
                {
                    ContentPath = @"content\",
                    RelativePath = true
                },
                Debug = new BubbasEngine.Engine.Debugging.DebugArgs() // Debug
                {
                    Activated = true,
                    DebugFontPath = @""
                },
                Input = new BubbasEngine.Engine.Input.InputSettingsArgs() // Input 
                {
                    FocusedInputOnly = true,
                    FocusedGamePadInputOnly = false,
                    FocusedKeyoardInputOnly = false,
                    FocusedMouseInputOnly = false
                },
                Graphics = new BubbasEngine.Engine.Graphics.Rendering.GraphicsRendererArgs() // Graphics
                {
                    ResolutionWidth = (int)((float)winWidth * resScale),
                    ResolutionHeight = (int)((float)winHeight * resScale)
                },
                Time = new BubbasEngine.Engine.Timing.TimeManagerArgs() // Time
                {
                    StepsPerSecond = 60
                },
                Window = new BubbasEngine.Engine.Windows.GameWindowArgs() // Window
                {
                    CreateWindow = true,
                    WindowWidth = winWidth,
                    WindowHeight = winHeight,
                    WindowTitle = "[Bubbas Engine] Example Game"//,
                    //WindowStyle = SFML.Window.Styles.Close
                }
            };

            for (int i = 0; i < 2; i++)
                RunGame(gameArgs);

            while (_gamesRunning > 0)
            {
                Thread.Sleep(0);
            }
        }

        private static void RunGame(GameArgs args)
        {
            Thread thread = new Thread((o) =>
                {
                    // Engine
                    GameEngine game = new GameEngine(args);

                    // Run Game
                    _gamesRunning++;
                    game.Run(new LaunchGameState());
                    _gamesRunning--;
                });
            thread.Start();
        }
    }
}
