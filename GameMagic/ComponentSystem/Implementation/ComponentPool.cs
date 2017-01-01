using System;
using System.Collections.Generic;
using GameMagic.Logging;

namespace GameMagic.ComponentSystem.Implementation
{
    class ComponentPool<T> : IComponentPool<T> where T : IPoolableComponent<T>, new()
    {
        private int _size;
        private readonly Dictionary<Guid, T> _components = new Dictionary<Guid, T>();
        private readonly Queue<T> _recycleQueue = new Queue<T>();
        private ISimpleLogger _logger;
        private IComponentStore _overflow;

        public ComponentPool(int size, IComponentStore overflowStore, ISimpleLogger logger)
        {
            _size = size;
            _overflow = overflowStore;
            _logger = logger;
            for (int i = 0; i < _size; i++)
            {
                var c = new T();
                c.RegisterWithPool(this);
                _components.Add(c.ID, c);
                _recycleQueue.Enqueue(c);
            }
        }

        public T FindComponent(Guid id)
        {
            if (_components.ContainsKey(id))
            {
                return _components[id];
            }

            return _overflow.FindComponent<T>(id);
        }

        public T GetBlankComponent()
        { 
            if (_recycleQueue.Count > 0)
            {
                var c = _recycleQueue.Dequeue();
                c.Reset();
                return c;
            }
            else
            {
                _logger.LogError("Component Pool Overflow!");
                var c = new T();
                c.RegisterWithPool(this);
                _overflow.AddComponent(c);
                return c;
            } 
        }
    }
}
