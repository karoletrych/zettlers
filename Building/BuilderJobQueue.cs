using System.Collections.Generic;

namespace zettlers
{
    class BuilderJobQueue
    {
        public List<BuildJob> Queue =>
            new List<BuildJob>(_queue);
        private readonly List<BuildJob> _queue = 
            new List<BuildJob>();
        public void Enqueue(BuildJob job)
        {
            _queue.Add(job);
        }
    }
}
