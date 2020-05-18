using System.Collections.Generic;
using UnityEngine;

namespace zettlers
{
    class TakeProfessionJobQueue
    {
        private readonly Queue<TakeProfessionJob> _queue = new Queue<TakeProfessionJob>();
        public TakeProfessionJob Dequeue(Vector2Int pos)
        {
            return _queue.Dequeue();
        }

        public void Enqueue(TakeProfessionJob job)
        {
            _queue.Enqueue(job);
        }
    }
}
