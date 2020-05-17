namespace zettlers
{
    class BuildingBuiltEventHandler : IGameEventHandler<BuildingBuilt>
    {
        private readonly TakeProfessionJobQueue _jobQueue;
        public void Handle(BuildingBuilt @event)
        {
            _jobQueue.Enqueue(new TakeProfessionJob
            {
                Building = @event.Building
            });
        }
    }
}
