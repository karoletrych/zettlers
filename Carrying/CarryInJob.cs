namespace zettlers
{
    class CarryInJob : IJob
    {
        public Building Target { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
