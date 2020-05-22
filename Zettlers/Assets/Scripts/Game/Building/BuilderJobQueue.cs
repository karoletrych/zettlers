using System.Collections.Generic;
using Unity.Collections;

namespace zettlers
{
    public static class BuilderJobQueue
    {
        public static readonly Queue<BuildJob> Queue = new Queue<BuildJob>();
    }
}
