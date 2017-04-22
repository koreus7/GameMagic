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
        Vector2 dir;

        public TestEntity(World world, Vector2 pos) : base(world, pos)
        {
        }

        public override void Init()
        {
            dir = Rand.Inst.Vec2(10);
            var sr = this.AddNewComponent<SpriteRenderer>();
            sr.tex = StaticImg.asprite;
            var r = this.AddNewComponent<RectColider>();
            r.WatchCollisions = true;
            r.rect = new Rectangle(0, 0, sr.tex.Width, sr.tex.Height);
            this.AddNewComponent<Wander>();
        }
    }
}
