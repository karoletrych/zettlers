using UnityEngine;

namespace zettlers
{
    class Builder : IZettler
    {
        public Vector2Int Position { get; set; }
        public BuildJob Job { get; set; }
    }
}
