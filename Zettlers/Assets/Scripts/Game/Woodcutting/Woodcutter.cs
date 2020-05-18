using UnityEngine;

namespace zettlers
{
    class Woodcutter : IZettler
    {
        public Building Building { get; set; }
        public Vector2Int WorkArea { get; set; }
        public CutTreeJob Job { get; set; }
    }
}
