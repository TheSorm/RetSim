using RetSim.Events;

namespace RetSim.EventQueues
{
    public interface IEventQueue
    {
        public void Add(Event e);
        public void AddRange(List<Event> events);
        public Event GetNext();
        public Event RemoveNext();
        public bool IsEmpty();
        public void EnsureSorting();
        public void UpdateRemove(Event e);
        public void UpdateAdd(Event e);
    }
}
