using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using BubbasEngine.Engine.Physics.Common;

namespace BubbasEngine.Engine
{
    public static class UnitConverter
    {
        //
        public static float ScreenPositionScale(float screenSize, float pos)
        {
            return pos / screenSize;
        }
        public static Vector2f Screen(Vector2f screenSize, Vector2f pos)
        {
            return pos / screenSize;
        }

        // Physics/Screen Ratio
        public static Vector2 ScreenToPhysicsRatio(Vector2u screenSize, Vector2f worldSize)
        {
            return new Vector2(worldSize.X, worldSize.Y) / new Vector2(screenSize.X, screenSize.Y);
        }
        public static Vector2f PhysicsToScreenRatio(Vector2u screenSize, Vector2f worldSize)
        {
            return new Vector2f(screenSize) / worldSize;
        }

        // Physics/Screen Position
        public static Vector2 ScreenToPhysicsPosition(Vector2f screenPos, Vector2u screenSize, Vector2f worldSize)
        {
            return new Vector2(screenPos.X, screenPos.Y) * ScreenToPhysicsRatio(screenSize, worldSize);
        }
        public static Vector2f PhysicsToScreenPosition(Vector2 worldPos, Vector2u screenSize, Vector2f worldSize)
        {
            return new Vector2f(worldPos.X, worldPos.Y) * PhysicsToScreenRatio(screenSize, worldSize);
        }
    }
}
