using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class Edge
    {
        private Vertex sourceVertex;
        private Vertex destinationVertex;
        private int cost;

        public Vertex SourceVertex => sourceVertex;
        public Vertex DestinationVertex => destinationVertex;
        public int Cost => cost;

        public Edge(Vertex source, Vertex destination, int cost )
        {
            sourceVertex = source;
            destinationVertex = destination;
            this.cost = cost;
        }
    }
    
}

