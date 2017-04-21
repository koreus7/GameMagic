using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem.Implementation;
using GameMagic.Entities;

namespace GameMagic
{
    class MainWorld: World
    {
        public override void Init()
        {
            this.AddEntity(new TestEntity(this));
            this.AddEntity(new TestEntity(this));
            this.AddEntity(new TestEntity(this));
            this.AddEntity(new TestEntity(this));
            this.AddEntity(new TestEntity(this));
            this.AddEntity(new TestEntity(this));
            this.AddEntity(new TestEntity(this));
            this.AddEntity(new TestEntity(this));
        }
    }
}
