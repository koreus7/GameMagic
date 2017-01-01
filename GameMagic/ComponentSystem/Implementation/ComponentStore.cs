using System;
using System.Collections.Generic;

namespace GameMagic.ComponentSystem.Implementation
{
    class ComponentStore : IComponentStore
    {
        private readonly Dictionary<Guid, IComponent> _components = new Dictionary<Guid, IComponent>();

        public void AddComponent(IComponent component)
        {
            _components.Add(component.ID, component);
        }

        public T FindComponent<T>(Guid componentID) where T : IComponent
        {
            return (T) _components[componentID];
        }
    }
}
