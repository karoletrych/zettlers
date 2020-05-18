// using System.Collections.Generic;
// using System.Linq;
// using Unity.Entities;

// namespace zettlers
// {
//     class PlantTreeJobAssigningSystem : ComponentSystem
//     {
//         public void Process()
//         {
//             IEnumerable<Forester> freeForesters = 
//                 _zettlerList.GetZettlers<Forester>().Where(f => f.Job == null);
//             foreach (Forester forester in freeForesters)
//             {
//                 forester.Job = new PlantTreeJob {
//                     TargetPosition = forester.WorkArea
//                 };
//             }            
//         }
//     }
// }
