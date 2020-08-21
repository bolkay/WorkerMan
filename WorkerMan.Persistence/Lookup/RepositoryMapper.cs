using System.Collections.Generic;
using System.Linq;
using WorkerMan.CrossCutting.Entities.Interfaces;
using WorkerMan.Persistence.Interfaces;

namespace WorkerMan.Persistence.Lookup
{
    public class RepositoryMapper<TKey> : Dictionary<TKey, dynamic>
        where TKey : class, IEntity
    {
        public RepositoryMapper<TKey> AddEntityRepositoryMap(TKey key, dynamic value)
        {
            if (ObjectsAreOkay(key, value))
                Add(key, value);

            return this;
        }

        public dynamic FindRepository<TEntity>() where TEntity : class, IEntity
        {
            return this.FirstOrDefault(x => x.Key.GetType() == typeof(TEntity)).Value;
        }
        private bool ObjectsAreOkay(params object[] @objects)
        {
            return objects.Any(x => x != null);
        }
    }
}
