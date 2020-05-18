using System;
using System.Collections.Generic;
using System.Linq;
// using SimpleInjector;

namespace zettlers
{
    class PlayerCommandBus
    {
        static Dictionary<PlayerCommandType, IPlayerCommandHandler> commandHandlers =
            new Dictionary<PlayerCommandType, IPlayerCommandHandler>
        {
            { PlayerCommandType.BuildBuilding, new BuildBuildingCommandHandler()}
        };

        Queue<IPlayerCommand> _queue = new Queue<IPlayerCommand>();

        public void Post(IPlayerCommand @event)
        {
            _queue.Enqueue(@event);
        }
        public void ProcessEvents()
        {
            while(_queue.Count > 0)
            {
                IPlayerCommand command = _queue.Dequeue();
                if (command is BuildBuildingCommand buildCommand)
                {
                    (commandHandlers[PlayerCommandType.BuildBuilding] as IPlayerCommandHandler<BuildBuildingCommand>)
                        .Handle(buildCommand);
                }
            }
        }
    }
}
