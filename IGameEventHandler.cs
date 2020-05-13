namespace zettlers
{
    interface IGameEventHandler<TEvent> where TEvent : IGameEvent
    {
        void Handle(TEvent @event);
    }
}
