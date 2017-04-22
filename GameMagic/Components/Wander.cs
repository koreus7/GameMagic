using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Components
{
    class Wander : IComponent
    {
        public int ID { get; set; }
        public IEntity Entity { get; set; }

        private Vector2 dir;

        public void Init()
        {
            dir = Rand.Inst.Vec2(2);
        }

        public void Update(GameTime gameTime)
        {
            Vector2 projected = Entity.Position + dir*gameTime.ElapsedGameTime.Milliseconds/5;
            if (projected.X > Entity.World.Width)
            {
                projected.X = 0;
            }
            else if(projected.X < 0)
            {
                projected.X = Entity.World.Width;
            }

            if (projected.Y > Entity.World.Height)
            {
                projected.Y = 0;
            }
            else if (projected.Y < 0)
            {
                projected.Y = Entity.World.Height;
            }

            Entity.SetPosition(projected);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
         
        }
    }
}
