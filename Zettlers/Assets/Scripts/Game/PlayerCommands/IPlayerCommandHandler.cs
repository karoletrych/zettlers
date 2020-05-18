namespace zettlers
{

    interface IPlayerCommandHandler
    {
    }
    interface IPlayerCommandHandler<TPlayerCommand> : IPlayerCommandHandler where TPlayerCommand : IPlayerCommand
    {
        void Handle(TPlayerCommand command);
    }
}
