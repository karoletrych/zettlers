using System;

namespace zettlers
{
    class CarrySystem : ISystem
    {
        private readonly ZettlersList _zettlersList;
        private readonly EventBus _eventBus;

        public CarrySystem(ZettlersList zettlersList, EventBus eventBus)
        {
            _zettlersList = zettlersList;
            _eventBus = eventBus;
        }

        public void Process()
        {
            foreach(Carrier carrier in _zettlersList.GetZettlers<Carrier>())
            {
                if(carrier.Job != null)
                {
                    if(true) // carried resource in
                    {
                        _eventBus.Post(new BuildResourceCarriedInEvent {
                            Building = carrier.Job.Target,
                            ResourceType = carrier.Job.ResourceType
                        });
                    }
                }
            }
        }
    }
}