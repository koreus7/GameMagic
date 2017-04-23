﻿using System;
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
            this.AddEntity(new MouseEntity(this, Vector2.One));
        }

        public MainWorld(GMGame game) : base(game)
        {
        }
    }
}
