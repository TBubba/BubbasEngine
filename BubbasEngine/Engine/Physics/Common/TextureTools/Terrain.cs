using System.Collections.Generic;
using BubbasEngine.Engine.Physics.Dynamics;
using BubbasEngine.Engine.Physics.Collision;
using BubbasEngine.Engine.Physics.Factories;
using BubbasEngine.Engine.Physics.Common.Decomposition;
using BubbasEngine.Engine.Physics.Common.PolygonManipulation;

namespace BubbasEngine.Engine.Physics.Common.TextureTools
{
    public enum TerrainCellState : byte
    {
        Empty,
        Modified,
        Full
    }

    public class TerrainCell
    {
        //
        private Body _body;

        //
        public Body Body;

        //
        public TerrainCell()
        {
            
        }
    }

    public class Terrain
    {
        //
        private PhysicsWorld _world;
        private TerrainCellState[,] _cellStateMap;
        private List<Body>[,] _cellBodies;

        private int _cellWidth;
        private int _cellHeight;
        private int _cellsHor;
        private int _cellsVer;

        //
        public PhysicsWorld World
        { get { return _world; } }

        public int CellWidth
        { get { return _cellWidth; } }
        public int CellHeight
        { get { return _cellHeight; } }
        public int CellsHor
        { get { return _cellsHor; } }
        public int CellsVer
        { get { return _cellsVer; } }

        //
        public Terrain(PhysicsWorld world, int cellWidth, int cellHeight, int cellsHor, int cellsVer)
        {
            _world = world;
            _cellWidth = cellWidth;
            _cellHeight = cellHeight;
            _cellsHor = cellsHor;
            _cellsVer = cellsVer;
        }

        //
        public void Initialize()
        {
            //
            _cellStateMap = new TerrainCellState[_cellsHor, _cellsVer];
            _cellBodies = new List<Body>[_cellWidth, _cellHeight];

            //
            
        }

        public void AddToWorld()
        {

        }
    }
}