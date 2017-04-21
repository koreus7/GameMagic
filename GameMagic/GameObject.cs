using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;

namespace GameMagic
{
    class GameObject : Entity
    {
        public Vector2 Position { get; set; }

        public GameObject(World world) : base(world)
        {
        }
    }
}
