using System;

namespace zettlers
{
    class Building
    {
        public Guid Id { get; set; }
        public BuildingType Type { get; set; }
        public Vector2 Pos { get; set; }
    }
}
