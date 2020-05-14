namespace zettlers
{
    class Carrier : IZettler
    {
        public Vector2 Pos { get; set; }
        public CarryInJob Job { get; set; }
    }
}
