namespace ProjectRLG.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using ProjectRLG.Contracts;

    public class ActorQueue
    {
        private List<IActor> _data;

        public ActorQueue()
        {
            _data = new List<IActor>();            
        }

        public int Count
        {
            get
            {
                return _data.Count;
            }
        }

        public IActor Dequeue()
        {
            IActor resultActor = _data[_data.Count - 1];
            _data.RemoveAt(_data.Count - 1);

            SortQueue();

            return resultActor;
        }
        public void Enqueue(IActor actor)
        {
            _data.Add(actor);
                SortQueue();
        }
        public IActor Peek()
        {
            return _data[_data.Count - 1];
        }
        public bool Contains(IActor actor)
        {
            return _data.Contains(actor);
        }
        public void Clear()
        {
            _data = new List<IActor>();
        }

        private void SortQueue()
        {
            _data.Sort((x, y) => y.Energy.CompareTo(x.Energy));
        }
    }
}
