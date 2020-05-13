using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;

namespace zettlers
{
    class EventBus
    {
        public EventBus(Container container)
        {
            this.container = container;
        }
        Queue<IGameEvent> _queue = new Queue<IGameEvent>();
        private readonly Container container;

        public void Post(IGameEvent @event)
        {
            _queue.Enqueue(@event);

        }
        public void ProcessEvents()
        {
            while(_queue.TryDequeue(out IGameEvent @event))
            {
                IEnumerable<Type> handlerTypes = 
                    typeof(Program).Assembly.GetTypes()
                    .Where(t=>t.GetInterfaces().Any(i => IsEventHandlerOfType(i, @event.GetType())));

                
                foreach(Type handlerType in handlerTypes)
                {
                    dynamic handler = container.GetInstance(handlerType);
                    handler.Handle((dynamic)@event);
                }
            }
        }

        private bool IsEventHandlerOfType(Type i, Type eventType)
        {
            return i.IsGenericType 
                && i.GetGenericTypeDefinition() == typeof(IGameEventHandler<>) 
                && i.GetGenericArguments().First() == eventType;
        }
    }
}
