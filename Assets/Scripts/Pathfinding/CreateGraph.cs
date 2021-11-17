using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class CreateGraph : MonoBehaviour 
    {
        private Graph graph;
        private GridCell[,] gridCellArray;
        private Vertex[,] verticeArray;

        public Graph Graph => graph;
        public Vertex[,] VerticesArray => verticeArray;
        public void FillGraph(Grid grid)
        {
            //make corresponding 2d array but fill with vertices(that holds the grid cell),
            //every vertex get edges (with source, target, cost). total saves in hashset in graph,
            //and specific in hashset in the vertex.
            gridCellArray = grid.GridArray;
            verticeArray = new Vertex[gridCellArray.GetLength(0), gridCellArray.GetLength(1)];
            for (int x = 0; x < gridCellArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridCellArray.GetLength(1); y++)
                {
                    verticeArray[x, y] = new Vertex(gridCellArray[x, y], graph);
                }
            }

            for (int x = 0; x < verticeArray.GetLength(0); x++)
            {
                for (int y = 0; y < verticeArray.GetLength(1); y++)
                {
                    if (IsInBounds(x + 1, y))
                    {
                        graph.AddEdgeToGraph(verticeArray[x,y], verticeArray[x + 1, y], 1);
                    }
                    else
                    {
                        graph.AddEdgeToGraph(verticeArray[x,y], verticeArray[0,y], 1);
                    }

                    if (IsInBounds(x, y + 1))
                    {
                        graph.AddEdgeToGraph(verticeArray[x,y], verticeArray[x, y + 1], 1);
                    }
                    else
                    {
                        graph.AddEdgeToGraph(verticeArray[x,y], verticeArray[x, 0], 1);
                    }

                    if (IsInBounds(x - 1, y))
                    {
                        graph.AddEdgeToGraph(verticeArray[x,y], verticeArray[x - 1, y], 1);
                    }
                    else
                    {
                        graph.AddEdgeToGraph(verticeArray[x,y], verticeArray[verticeArray.GetLength(0)-1, y], 1);
                    }

                    if (IsInBounds(x, y - 1))
                    {
                        graph.AddEdgeToGraph(verticeArray[x,y], verticeArray[x, y - 1], 1);
                    }

                    else
                    {
                        graph.AddEdgeToGraph(verticeArray[x,y], verticeArray[x,verticeArray.GetLength(1)-1], 1);
                    }
                }
            }
        }

        private bool IsInBounds(int x, int y)
        {
            if (x < 0 || y < 0 || x > verticeArray.GetLength(0) -1 || y > verticeArray.GetLength(1) -1 )
            {
                return false;
            }
            return true;
        }

        public void CreateTheGraph()
        {
            graph = new Graph();
        }
    }
}

