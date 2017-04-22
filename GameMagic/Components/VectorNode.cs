using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem;
using GameMagic.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Components
{
    class VectorNode : IComponent
    {
        public int ID { get; set; }
        public IEntity Entity { get; set; }
        public Vector2 Val { get; set; }

        private RectColider r;

        public void Init()
        {
            r = Entity.GetComponent<RectColider>();
            Val = Rand.Inst.Vec2(5);
        }

        public void Update(GameTime gameTime)
        {
            r.WatchCollisions = true;

            foreach (RectColider rectColider in r.Collisions)
            {
                if (rectColider.Entity is TestEntity)
                {
                    rectColider.Entity.GetComponent<Wander>().SetDir(Val);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Entity.World.Game.DrawRectangle(new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, 50,50), Color.Orange);
        }

        public int BatchNo => 2;
    }
}
