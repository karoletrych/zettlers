// using System.Collections.Generic;
// using System.Linq;
// using Unity.Entities;

// namespace zettlers
// {
//     class CutTreeJobAssigningSystem : SystemBase
//     {
//         public void Process()
//         {
//             IEnumerable<Woodcutter> freeWoodcutters = 
//                 _zettlerList.GetZettlers<Woodcutter>().Where(f => f.Job == null);
//             foreach (Woodcutter woodcutter in freeWoodcutters)
//             {
//                 woodcutter.Job = new CutTreeJob {
//                     Position = woodcutter.WorkArea
//                 };
//             }            
//         }
//     }
// }
