namespace zettlers
{
    class Stonecutter : IZettler
    {
        public Building Building { get; set; }
        public Vector2 WorkArea { get; set; }
        public CutStoneJob? Job { get; set; }
    }
}
