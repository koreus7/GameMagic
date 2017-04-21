using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.ComponentSystem.Implementation
{
    public class Entity : IEntity
    {
        private readonly World _world;

        public Entity(World world)
        {
            _world = world;
        }

        public T GetComponent<T>() where T : IComponent
        {
            return _world.GetComponent<T>(this.ID);
        }

        public T AddNewComponent<T>() where T : IComponent, new()
        {
            return _world.NewComponent<T>(ID);
        }

        public virtual void Init()
        {

        }

        public int ID { get; set; }    
    }
}
