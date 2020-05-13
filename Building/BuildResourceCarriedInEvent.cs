namespace zettlers
{
    class BuildResourceCarriedInEvent : IGameEvent
    {
        public Building Building { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
