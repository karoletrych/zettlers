namespace zettlers
{
    class BuildResourceCarriedInEventHandler : IGameEventHandler<BuildResourceCarriedInEvent>
    {
        private readonly BuilderJobQueue _jobQueue;
        public BuildResourceCarriedInEventHandler(BuilderJobQueue jobQueue)
        {
            _jobQueue = jobQueue;
        }

        public void Handle(BuildResourceCarriedInEvent @event)
        {
            _jobQueue.Enqueue(new BuildJob
            {
                ResourceType = @event.ResourceType,
                Building = @event.Building
            });
        }
    }
}
