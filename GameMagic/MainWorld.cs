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
            int tilesize = 50;

            for (int i = 0; i < Width; i+= tilesize)
            {
                for (int j = 0; j < Height; j+= tilesize)
                {
                    this.AddEntity(new ForceNode(this, new Vector2(i, j), new Rectangle(0, 0, tilesize, tilesize)));
                }
            }


            for (int i = 0; i < 50; i++)
            {
                this.AddEntity(new TestEntity(this, new Vector2(Rand.Inst.Int(Width), Rand.Inst.Int(Height))));
            }
        }

        public MainWorld(GMGame game) : base(game)
        {
        }
    }
}
