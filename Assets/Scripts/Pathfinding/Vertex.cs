using System.Collections.Generic;

namespace Snakey
    {
        public class Vertex
        {
            private GridCell value;
            private HashSet<Edge> _edgesToVertexToVertex;
            private Vertex previousVertex;
            private int indexInGraph;
            private float distance;
            private bool isVisited;
    
            public bool IsVisited
            {
                get => isVisited;
                set => isVisited = value;
            }
            public GridCell Value
            {
                get => value;
                set => this.value = value;
            }
            public float Distance
            {
                get => distance;
                set => distance = value;
            }
            public Vertex PreviousVertex
            {
                get => previousVertex;
                set => previousVertex = value;
            }
            public HashSet<Edge> EdgesToVertex => _edgesToVertexToVertex;

            public Vertex(GridCell value, Graph graph)
            {
                this.value = value;
                _edgesToVertexToVertex = new HashSet<Edge>();
                graph.AddVertex(this);
                indexInGraph = graph.NumberOfVertices;
            }
            
            /// <summary>
            /// Creates a new edge between source-vertex and destination-vertex with a cost. Adds it to the vertex hashset.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="cost"></param>
            /// <returns>Edge</returns>
            public Edge AddEdgeToVertex(Vertex source, Vertex destination, int cost)
            {
                Edge edge = new Edge(source, destination, cost );
                _edgesToVertexToVertex.Add(edge);
                return edge;
            }
        }
    }


