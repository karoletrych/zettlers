using UnityEngine;

namespace zettlers
{
    class Carrier : IZettler
    {
        public Vector2Int Pos { get; set; }
        public CarryInJob Job { get; set; }
    }
}
