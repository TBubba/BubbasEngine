using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Input
{
    public class InputSettingsArgs
    {
        public bool FocusedInputOnly;
        public bool FocusedMouseInputOnly;
        public bool FocusedKeyoardInputOnly;
        public bool FocusedGamePadInputOnly;

        // Constructor(s)
        public InputSettingsArgs()
        {
        }
        public InputSettingsArgs(InputSettingsArgs args)
        {
            FocusedInputOnly = args.FocusedInputOnly;
            FocusedMouseInputOnly = args.FocusedMouseInputOnly;
            FocusedKeyoardInputOnly = args.FocusedKeyoardInputOnly;
            FocusedGamePadInputOnly = args.FocusedGamePadInputOnly;
        }
    }
}
