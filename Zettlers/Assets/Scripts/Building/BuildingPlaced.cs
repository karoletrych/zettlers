namespace zettlers
{
    class BuildingPlaced : IPlayerCommand
    {
        public BuildingType BuildingType { get; set; }
        public Vector2 Pos { get; set; }
    }
}
