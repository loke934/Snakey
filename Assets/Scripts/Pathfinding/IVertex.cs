using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public interface IVertex //Todo delete
    {
        public GridCell Value { get; set; }
        public Vertex PreviousVertex { get; set; }
        public HashSet<Edge> EdgesToVertex { get; }
        public int IndexInGraph { get; }
        public float Distance { get; set; }
    }
}

