using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.GameStates
{
    public class GameStateManagerArgs
    {
        //
        public GameStateCondition DefaultCondition;

        // Constructor(s)
        public GameStateManagerArgs()
        {
            DefaultCondition = new GameStateCondition()
                {
                    BeginFrame = true,
                    Step = true,
                    Draw = true
                };
        }
        public GameStateManagerArgs(GameStateManagerArgs args)
        {
            DefaultCondition = new GameStateCondition(args.DefaultCondition);
        }

        //
    }
}
