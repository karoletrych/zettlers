// using System.Collections.Generic;
// using System.Linq;
// using Unity.Entities;

// namespace zettlers
// {
//     class CutStoneJobAssigningSystem : ComponentSystem
//     {
//         public void Process()
//         {
//             IEnumerable<Stonecutter> freeStonecutters = 
//                 _zettlerList.GetZettlers<Stonecutter>().Where(f => f.Job == null);
//             foreach (Stonecutter stonecutter in freeStonecutters)
//             {
//                 stonecutter.Job = new CutStoneJob {
//                     Position = stonecutter.WorkArea
//                 };
//             }            
//         }
//     }
// }
