using SimpleInjector;

namespace zettlers
{

    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.RegisterSingleton<CarrierJobQueue>();
            container.RegisterSingleton<ZettlersList>();
            container.RegisterSingleton<ResourcePriorityList>();

            container.Register<CarrierJobAssignerSystem>();
            container.Register<BuildingPlacedEventHandler>();
            container.Register<EventBus>();

            container.Verify();

            ZettlersList zettlersList = container.GetInstance<ZettlersList>();
            zettlersList.Add(new Carrier{
                Pos = new Vector2(0,0)
            });

            EventBus eventBus = container.GetInstance<EventBus>();
            CarrierJobAssignerSystem carrierAssignerSystem = container.GetInstance<CarrierJobAssignerSystem>();

            eventBus.Post(new BuildingPlaced
            {
                BuildingType = BuildingType.WoodcuttersHut,
                Pos = new Vector2(1, 1)
            });

            eventBus.Post(new BuildingPlaced
            {
                BuildingType = BuildingType.ForesterHut,
                Pos = new Vector2(10, 1)
            });

            eventBus.Post(new BuildingPlaced
            {
                BuildingType = BuildingType.StonecuttersHut,
                Pos = new Vector2(10, 1)
            });

            eventBus.Post(new BuildingPlaced
            {
                BuildingType = BuildingType.LumberMill,
                Pos = new Vector2(10, 1)
            });

            eventBus.ProcessEvents();
            carrierAssignerSystem.Process();

        }
    }
}
