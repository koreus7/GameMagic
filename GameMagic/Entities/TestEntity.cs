using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Entities
{
    class TestEntity : Entity
    {
        public TestEntity(World world, Vector2 pos) : base(world, pos)
        {
        }

        public override void Init()
        {
            var sr = this.AddNewComponent<SpriteRenderer>();
            sr.tex = StaticImg.abc;
        }
    }
}
