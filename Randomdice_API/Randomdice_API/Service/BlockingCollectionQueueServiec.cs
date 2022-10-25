using System.Collections.Concurrent;

namespace Randomdice_API.Service
{
    public class BlockingCollectionQueueServiec<T>
    {
        private BlockingCollection<T> _queue = new BlockingCollection<T>();

        public bool isEmpty()
        {
            return _queue.Count == 0;
        }

        public T pull()
        {
            return _queue.First();
        }

        public void push(T t)
        {
            _queue.Add(t);
        }
    }
}
