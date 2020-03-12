using System.Collections.Generic;

namespace MNG.Infrastructure
{
    public class ResourcesDefinition<T> where T : class, new()
    {
        private IList<T> _resources = new List<T>();

        public IEnumerable<T> Resources
        {
            get { return _resources; }
        }

        public void Add(T item)
        {
            if (!_resources.Contains(item))
            {
                _resources.Add(item);
            }
        }
    }
}