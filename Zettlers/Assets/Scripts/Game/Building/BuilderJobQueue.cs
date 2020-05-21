using System.Collections.Generic;
using Unity.Collections;

namespace zettlers
{
    public static class BuilderJobQueue
    {
        public static NativeQueue<BuildJob> Queue = new NativeQueue<BuildJob>(Allocator.Persistent);
    }
}
