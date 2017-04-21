using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;
using GameMagic.ComponentSystem.Implementation;

namespace GameMagic.Entities
{
    class TestEntity : GameObject
    {
        public TestEntity(World world) : base(world)
        {
        }

        public override void Init()
        {
            this.AddNewComponent<SpriteRenderer>();
        }
    }
}
