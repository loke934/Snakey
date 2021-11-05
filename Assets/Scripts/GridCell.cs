using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class GridCell
    {
        public GridCell(int x, int y)
        {
            WorldPositionOfCell = new Vector3(x, y, 0f);
        }
        
        public Vector3 WorldPositionOfCell { get; }
    }
}
