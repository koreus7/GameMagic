﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;

namespace GameMagic.Entities
{
    class Source : Entity
    {
        public Source(World world, Vector2 pos) : base(world, pos)
        {
        }

        public override void Init()
        {
           // var sr = this.AddNewComponent<SpriteRenderer>();
           // sr.Center = true;
           // sr.BatchNo = 1;
           // sr.tex = StaticImg.sprite1024;
            var r = this.AddNewComponent<RectColider>();
            r.WatchCollisions = true;
            r.rect = new Rectangle(0, 0, 200, 200);

            var emitter = this.AddNewComponent<OrbEmiter>();

            for (int i = 0; i < 50; i++)
            {
                emitter.Emit();
            }
        }

    }
}
