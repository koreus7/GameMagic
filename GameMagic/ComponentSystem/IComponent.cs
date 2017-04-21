using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.ComponentSystem
{
    public interface IComponent : IIdentifiable
    {
        int GetType();
        IEntity Entity { get; set; }
    }
}
