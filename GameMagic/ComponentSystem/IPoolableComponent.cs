using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.ComponentSystem
{
    interface IPoolableComponent<T> : IComponent where T : IComponent
    {
        /// <summary>
        /// Reset the component so it can be reused.
        /// </summary>
        void Reset();

        /// <summary>
        /// Register with the pool so it knows when this component is ready to be reset.
        /// </summary>
        /// <param name="pool"></param>
        void RegisterWithPool(IComponentPool<T> pool);
    }
}
