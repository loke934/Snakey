using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public enum CellType
    {
        Obstacle,
        Walkable
    }

    public class GridCell
    {
        public CellType CellType { get; }
        public Vector3 WorldPositionOfCell { get; }

        public GridCell(float x, float y, CellType type = CellType.Walkable)
        {
            WorldPositionOfCell = new Vector3(x, y, 0f);
            CellType = type;
        }
    }
}