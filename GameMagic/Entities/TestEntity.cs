﻿using System;
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
            sr.tex = StaticImg.asprite;
            sr.Center = true;
            var r = this.AddNewComponent<RectColider>();
            r.WatchCollisions = true;
            int sizeMod = 200;
            r.rect = new Rectangle(-sr.tex.Width/2 + sizeMod, -sr.tex.Height/2 + sizeMod, sr.tex.Width - sizeMod, sr.tex.Height - sizeMod);
            this.AddNewComponent<Wander>();
        }
    }
}
