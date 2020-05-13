namespace zettlers
{
    class CarryOutJob : IJob
    {
        public Building Source { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
