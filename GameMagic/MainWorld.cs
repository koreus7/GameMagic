using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem.Implementation;
using GameMagic.Entities;
using Microsoft.Xna.Framework;

namespace GameMagic
{
    class MainWorld: World
    {
        public override void Init()
        {
            this.AddEntity(new TestEntity(this, Vector2.One));
            this.AddEntity(new TestEntity(this, new Vector2(100,100)));
        }
    }
}
