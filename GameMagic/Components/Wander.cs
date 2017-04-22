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
            float x = Entity.Position.X;
            float y = Entity.Position.Y;
            int w = Entity.World.Width;
            int h = Entity.World.Height;

            float theta = Noise.Generate(x/300.0f + 2000.0f, y/300.0f + 1000.0f);
            dir = new Vector2(5.0f*(float)Math.Cos(theta*2*Math.PI),-5.0f*(float)Math.Sin(theta*2*Math.PI));

            dir += new Vector2(x/w, y/h) * 0.1f;
            dir += new Vector2((w - x)/w, (h - y)/y) * 0.1f; 

            Vector2 projected = Entity.Position + dir*0.01f*gameTime.ElapsedGameTime.Milliseconds;
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
         //   this.dir = dir;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
         
        }

        public int BatchNo => 0;
    }
}
