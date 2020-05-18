using UnityEngine;

namespace zettlers
{
    class Forester : IZettler
    {
        public Building Building { get; set; }
        public Vector2Int WorkArea { get; set; }
        public PlantTreeJob Job { get; set; }
    }
}
