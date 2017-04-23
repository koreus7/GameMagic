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
            for (int i = 0; i < 100; i++)
            {
                //this.AddEntity(new TestEntity(this, new Vector2(Rand.Inst.Int(Width), Rand.Inst.Int(Height))));
                this.AddEntity(new TestEntity(this, new Vector2(Width/2.0f + Rand.Inst.Int(100) - 50, Height/2.0f + Rand.Inst.Int(100) - 50)));
            }

            this.AddEntity(new MouseEntity(this, Vector2.One));
        }

        public MainWorld(GMGame game) : base(game)
        {
        }
    }
}
