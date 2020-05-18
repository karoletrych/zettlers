using UnityEngine;

namespace zettlers
{
    class Stonecutter : IZettler
    {
        public Building Building { get; set; }
        public Vector2Int WorkArea { get; set; }
        public CutStoneJob Job { get; set; }
    }
}
