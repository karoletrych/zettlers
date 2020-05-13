namespace zettlers
{
    class BuildJob : IJob
    {
        public Building Building { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
