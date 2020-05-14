using System;
using System.Collections.Generic;
using System.Linq;
// using SimpleInjector;

namespace zettlers
{
    class EventBus
    {
        // public EventBus(Container container)
        // {
            // _container = container;
        // }
        Queue<IGameEvent> _queue = new Queue<IGameEvent>();
        // private readonly Container _container;

        public void Post(IGameEvent @event)
        {
            _queue.Enqueue(@event);
        }
        public void ProcessEvents()
        {
            while(_queue.Count > 0)
            {
                IGameEvent @event = _queue.Dequeue();
                IEnumerable<Type> handlerTypes = 
                    typeof(Program).Assembly.GetTypes()
                    .Where(t=>t.GetInterfaces().Any(i => IsEventHandlerOfType(i, @event.GetType())));

                
                foreach(Type handlerType in handlerTypes)
                {
                    // dynamic handler = container.GetInstance(handlerType);
                    // handler.Handle((dynamic)@event);
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
