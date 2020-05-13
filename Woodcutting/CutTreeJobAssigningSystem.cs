using System.Collections.Generic;
using System.Linq;

namespace zettlers
{
    class CutTreeJobAssigningSystem : ISystem
    {
        public CutTreeJobAssigningSystem(ZettlersList zettlersList)
        {
            _zettlerList = zettlersList;
        }

        private ZettlersList _zettlerList;

        public void Process()
        {
            IEnumerable<Woodcutter> freeWoodcutters = 
                _zettlerList.GetZettlers<Woodcutter>().Where(f => f.Job == null);
            foreach (Woodcutter woodcutter in freeWoodcutters)
            {
                woodcutter.Job = new CutTreeJob {
                    Position = woodcutter.WorkArea
                };
            }            
        }
    }
}
