using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.ComponentSystem
{
    interface IComponentPool<T> where T : IComponent
    {
        T FindComponent(Guid id);
        T GetBlankComponent();
    }
}
