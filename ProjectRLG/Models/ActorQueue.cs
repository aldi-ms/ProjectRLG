namespace ProjectRLG.Models
{
    using ProjectRLG.Contracts;
    using System;
    using System.Collections.Generic;

    public class ActorQueue
    {
        private List<IActor> data;

        public ActorQueue()
        {
            data = new List<IActor>();
        }

        public int Count
        {
            get
            {
                return data.Count;
            }
        }

        public IActor Dequeue()
        {
            IActor resultActor = data[data.Count];
            data.RemoveAt(data.Count);

            Sort();

            return resultActor;
        }
        public void Enqueue(IActor actor)
        {
            data.Add(actor);

            Sort();
        }        
        public IActor Peek()
        {
            return data[data.Count];
        }
        public bool Contains(IActor actor)
        {
            return data.Contains(actor);
        }
        public void Clear()
        {
            data = new List<IActor>();
        }

        private void Sort()
        {
            data.Sort((x, y) => -x.Energy.CompareTo(y.Energy));
        }
    }
}
