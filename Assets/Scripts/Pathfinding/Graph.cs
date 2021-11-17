using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class Graph
    {
        private List<Vertex> allVerticesInGraph;
        private HashSet<Edge> allEdgesInGraph;

        public int NumberOfVertices => allVerticesInGraph.Count;
        public int NumberOfEdges => allEdgesInGraph.Count; //Todo delete if never used
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

        public Stack<Vertex> FindLeastCostBetweenVertices(Vertex[,] array, Vector2Int start, 
            Vector2Int target) //Add current direction and figure out 
        {
            foreach (Vertex vertex in array)
            {
                vertex.Distance = Mathf.Infinity;
                vertex.IsVisited = false;
            }
            Vertex targetVertex = array[target.x, target.y];
            Vertex startVertex = array[start.x, start.y];
            //Todo remove when all works
            Debug.DrawLine(startVertex.Value.WorldPositionOfCell, targetVertex.Value.WorldPositionOfCell, Color.magenta, 1000f);
            Queue<Vertex> queue = new Queue<Vertex>();
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

            Stack<Vertex> stack = new Stack<Vertex>();
            Vertex current = targetVertex;
            while (current != startVertex)
            {
                stack.Push(current);
                current = current.PreviousVertex;
            }
            return stack;
        }

       
    }
    
}

