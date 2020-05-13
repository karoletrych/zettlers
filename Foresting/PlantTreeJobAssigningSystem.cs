using System.Collections.Generic;
using System.Linq;

namespace zettlers
{
    class PlantTreeJobAssigningSystem : ISystem
    {
        public PlantTreeJobAssigningSystem(ZettlersList zettlersList)
        {
            _zettlerList = zettlersList;
        }

        private ZettlersList _zettlerList;

        public void Process()
        {
            IEnumerable<Forester> freeForesters = 
                _zettlerList.GetZettlers<Forester>().Where(f => f.Job == null);
            foreach (Forester forester in freeForesters)
            {
                forester.Job = new PlantTreeJob {
                    TargetPosition = forester.WorkArea
                };
            }            
        }
    }
}
