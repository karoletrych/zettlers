// using Unity.Entities;

// namespace zettlers
// {
//     class CarrySystem : ComponentSystem
//     {
//         public void Process()
//         {
//             foreach(Carrier carrier in _zettlersList.GetZettlers<Carrier>())
//             {
//                 if(carrier.Job != null)
//                 {
//                     if(true) // carried resource in
//                     {
//                         _eventBus.Post(new BuildResourceCarriedInEvent {
//                             Building = carrier.Job.TargetBuildingId,
//                             ResourceType = carrier.Job.ResourceType
//                         });
//                     }
//                 }
//             }
//         }
//     }
// }