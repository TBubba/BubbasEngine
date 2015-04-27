using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Input;
using BubbasEngine.Engine.Windows;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Timing;
using BubbasEngine.Engine.GameStates;
using BubbasEngine.Engine.Graphics;
using BubbasEngine.Engine.Debugging;

namespace BubbasEngine.Engine
{
    public class GameArgs
    {
        // Other Compontents
        public InputSettingsArgs Input;
        public ContentManagerArgs Content;
        public GraphicsRendererArgs Graphics;
        public GameStateManagerArgs States;
        public TimeManagerArgs Time;
        public GameWindowArgs Window;
        public DebugArgs Debug;

        // Constructor(s)
        public GameArgs()
        {
            Input = new InputSettingsArgs();
            Content = new ContentManagerArgs();
            Graphics = new GraphicsRendererArgs();
            States = new GameStateManagerArgs();
            Time = new TimeManagerArgs();
            Window = new GameWindowArgs();
            Debug = new DebugArgs();
        }
        public GameArgs(GameArgs args)
        {
            Input = new InputSettingsArgs(args.Input);
            Content = new ContentManagerArgs(args.Content);
            Graphics = new GraphicsRendererArgs(args.Graphics);
            States = new GameStateManagerArgs(args.States);
            Time = new TimeManagerArgs(args.Time);
            Window = new GameWindowArgs(args.Window);
            Debug = new DebugArgs(args.Debug);
        }
    }
}
