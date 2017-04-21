using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameMagic.ComponentSystem.Implementation
{
    class ComponentStore
    {
        private readonly Dictionary<int, IComponent> _components = new Dictionary<int, IComponent>();
        private Dictionary<Type, int> ComponentTypeLookup = new Dictionary<Type, int>();
        private int componentIdCounter = 1;

        public T AddComponent<T>(int entityID) where T : IComponent, new()
        {
            var component = new T();
            component.ID = componentIdCounter;
            componentIdCounter++;
            _components.Add(Hash(entityID, ComponentTypeLookup[typeof(T)]), component);
            return component;
        }

        public T FindComponent<T>(int entityID) where T : IComponent
        {
            return (T) _components[Hash(entityID, ComponentTypeLookup[typeof(T)])];
        }

        private int Hash(int a, int b)
        {
            return (a + b)*(a + b + 1)/2 + b;
        }
    }
}
