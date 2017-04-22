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
            for (int i = 0; i < 50; i++)
            {
                this.AddEntity(new TestEntity(this, new Vector2(Rand.Inst.Int(Width), Rand.Inst.Int(Height))));
            }

            this.AddEntity(new MouseEntity(this, Vector2.One));
        }

        public MainWorld(GMGame game) : base(game)
        {
        }
    }
}
