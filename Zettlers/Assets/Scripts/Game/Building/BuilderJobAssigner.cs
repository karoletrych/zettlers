using System.Collections.Generic;
using System.Linq;

namespace zettlers
{
    class BuilderJobAssigner
    {
        public BuilderJobAssigner(BuilderJobQueue jobQueue, ZettlersList builderList)
        {
            _jobQueue = jobQueue;
            _zettlersList = builderList;
        }
        private readonly BuilderJobQueue _jobQueue;
        private ZettlersList _zettlersList;

        public void AssignBuildersToJobs()
        {
            List<Builder> freeBuilders = _zettlersList.GetZettlers<Builder>().Where(c => c.Job == null).ToList();

            double minDist = float.MaxValue;
            foreach (BuildJob job in _jobQueue.Queue)
            {
                if (freeBuilders.Count == 0)
                    return;

                Builder minDistBuilder = freeBuilders[0];
                foreach (Builder builder in freeBuilders)
                {
                    double dist = builder.Pos.Dist(job.Building.Pos);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        minDistBuilder = builder;
                    }
                }

                minDistBuilder.Job = job;
                freeBuilders.Remove(minDistBuilder);
            }
        }
    }
}
