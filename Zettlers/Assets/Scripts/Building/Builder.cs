namespace zettlers
{
    class Builder : IZettler
    {
        public Vector2 Pos { get; set; }
        public BuildJob Job { get; set; }
    }
}
