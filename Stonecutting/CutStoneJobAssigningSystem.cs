using System.Collections.Generic;
using System.Linq;

namespace zettlers
{
    class CutStoneJobAssigningSystem : ISystem
    {
        public CutStoneJobAssigningSystem(ZettlersList zettlersList)
        {
            _zettlerList = zettlersList;
        }

        private ZettlersList _zettlerList;

        public void Process()
        {
            IEnumerable<Stonecutter> freeStonecutters = 
                _zettlerList.GetZettlers<Stonecutter>().Where(f => f.Job == null);
            foreach (Stonecutter stonecutter in freeStonecutters)
            {
                stonecutter.Job = new CutStoneJob {
                    Position = stonecutter.WorkArea
                };
            }            
        }
    }
}
