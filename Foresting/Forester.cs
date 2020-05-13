namespace zettlers
{
    class Forester : IZettler
    {
        public Building Building { get; set; }
        public Vector2 WorkArea { get; set; }
        public PlantTreeJob? Job { get; set; }
    }
}
