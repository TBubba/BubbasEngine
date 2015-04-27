using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.GameStates
{
    public class GameStateCondition
    {
        // Whether or not the state is signed up to do the following...
        public bool BeginFrame;
        public bool Step;
        public bool Draw;

        // Constructor(s)
        internal GameStateCondition()
        {
        }
        internal GameStateCondition(GameStateCondition con)
        {
            BeginFrame = con.BeginFrame;
            Step = con.Step;
            Draw = con.Draw;
        }
    }
}
