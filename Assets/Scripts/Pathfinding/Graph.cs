using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Snakey
{
    public class Graph
    {
        private List<Vertex> allVerticesInGraph;
        private HashSet<Edge> allEdgesInGraph;
        private Stack<GridCell> stack = new Stack<GridCell>();

        public int NumberOfVertices => allVerticesInGraph.Count;
       
        public Graph()
        {
            allVerticesInGraph = new List<Vertex>();
            allEdgesInGraph = new HashSet<Edge>();
        }
        
        public void AddVertex(Vertex vertex)
        {
            allVerticesInGraph.Add(vertex);
        }

        /// <summary>
        /// Internal call to method to creat and add edge to sourcevertex.
        /// This edge then adds to total amount of edges in the graph.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="cost"></param>
        public void AddEdgeToGraph(Vertex source, Vertex destination, int cost)
        {
            allEdgesInGraph.Add(source.AddEdgeToVertex(source,destination, cost));
        }

        public Stack<GridCell> Pathfinding(Vertex[,] array, Vector2Int start, Vector2Int target, 
            Direction currentDirection)  
        {
            stack.Clear();
            Vertex startVertex = array[start.x, start.y];
            Vertex targetVertex = array[target.x, target.y];
            Queue<Vertex> queue = new Queue<Vertex>();

            foreach (Vertex vertex in array)
            {
                vertex.Distance = Mathf.Infinity;
                vertex.IsVisited = false;
            }
           
            startVertex.Distance = 0;
            queue.Enqueue(startVertex);
            
            while (queue.Count > 0)
            {
                Vertex currentVertex = queue.Dequeue();
                if (currentVertex.IsVisited == false)
                {
                    currentVertex.IsVisited = true;
                }

                foreach (Edge edge in currentVertex.EdgesToVertex)
                {
                    Vertex oppositeDirNeighbour = null;
                    
                    if (currentVertex == startVertex)
                    {
                        if (IsOppositeDir(currentDirection, startVertex, edge.DestinationVertex))
                        {
                            oppositeDirNeighbour = edge.DestinationVertex;
                            continue;
                        }
                    }
                    
                    if (edge.DestinationVertex == oppositeDirNeighbour || IsExclude(edge.DestinationVertex))
                    {
                        continue;
                    }
                    
                    Vertex destinationVertex = edge.DestinationVertex;
                    float currentDistanceNCost = currentVertex.Distance + edge.Cost;
                    
                    if (currentDistanceNCost < destinationVertex.Distance )
                    {
                        destinationVertex.Distance = currentDistanceNCost;
                        destinationVertex.PreviousVertex = currentVertex;
                        queue.Enqueue(destinationVertex);
                    }
                }
            }

            
            Vertex current = targetVertex;

            while (current != startVertex)
            {
                if (current.Distance == Mathf.Infinity)
                {
                    continue;
                }
                stack.Push(current.Value);
                current = current.PreviousVertex;
            }
            return stack;
        }

        private bool IsExclude(Vertex vertex)
        {
            if (vertex.Value.CellType == CellType.Obstacle)
            {
                return true;
            }
            return false;
        }
        
         private bool IsOppositeDir(Direction direction, Vertex start, Vertex destination)
         {
             Vector2 up = new Vector2(0f,1f);
             Vector2 down = new Vector2(0f,-1f);
             Vector2 right = new Vector2(1f,0f);
             Vector2 left = new Vector2(-1f,0f);
             Vector2 dirToExclude = new Vector2(0,0);
             Direction currentDirection = direction;
             
             switch (currentDirection)
             {
                 case Direction.up:
                      dirToExclude = down;
                      break;
                 case Direction.right:
                     dirToExclude = left;
                     break;
                 case Direction.down:
                     dirToExclude = up;
                     break;
                 case Direction.left:
                     dirToExclude = right;
                     break;
             }
         
             Vector2 dir = start.Value.WorldPositionOfCell - destination.Value.WorldPositionOfCell;
             if (dir == dirToExclude)
             {
                 return true;
             }
             return false;
         }
    }
}

