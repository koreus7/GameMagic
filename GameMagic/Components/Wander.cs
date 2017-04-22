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

        private RectColider rect;

        public void Init()
        {
            dir = Rand.Inst.Vec2(2);
            rect = Entity.GetComponent<RectColider>();
        }

        public void Update(GameTime gameTime)
        {
            Vector2 projected = Entity.Position + dir*gameTime.ElapsedGameTime.Milliseconds/5;
            if (projected.X > Entity.World.Width)
            {
                projected.X = -rect.rect.Width;
            }
            else if(projected.X < -rect.rect.Width)
            {
                projected.X = Entity.World.Width;
            }

            if (projected.Y > Entity.World.Height)
            {
                projected.Y = -rect.rect.Height;
            }
            else if (projected.Y < -rect.rect.Height)
            {
                projected.Y = Entity.World.Height;
            }

            Entity.SetPosition(projected);
        }

        public void SetDir(Vector2 dir)
        {
            this.dir = dir;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
         
        }

        public int BatchNo => 0;
    }
}
