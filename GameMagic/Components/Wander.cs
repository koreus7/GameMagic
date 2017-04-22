using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem;
using GameMagic.ComponentSystem.Implementation;
using GameMagic.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            dir = new Vector2(5.0f*(float)Math.Cos(theta*2*Math.PI),-5.0f*(float)Math.Sin(theta*2*Math.PI)) * 1.5f;

            dir += new Vector2(x/w, y/h) * 0.1f;
            dir += new Vector2((w - x)/w, (h - y)/y) * 0.1f;
            MouseState m = Mouse.GetState();

            float mouseDistance = Vector2.Distance(Entity.Position, new Vector2(m.X, m.Y));
            if (mouseDistance < 200)
            {
                Vector2 delta = new Vector2(m.X - Entity.Position.X, m.Y - Entity.Position.Y);
                float mod = Math.Min(delta.LengthSquared(), 100.0f)/100.0f;
                dir += delta.Normalized() * 30.0f * mod;
            }

            foreach (RectColider colider in rect.Collisions)
            {
                if (colider.Entity is TestEntity)
                {
                    dir -= (colider.Entity.Position - Entity.Position).Normalized() * 3.0f;
                }
                else if (colider.Entity is Repelatron)
                {
                    Vector2 delta = -(colider.Entity.Position - Entity.Position);
                    float mod = (float) (10.0f*Math.Min(50.0, delta.LengthSquared()) / 50.0f);
                    dir += delta.Normalized()*mod;
                }
                else if (colider.Entity is Hub)
                {
                    Vector2 delta = colider.Entity.Position - Entity.Position;
                    float mod = Math.Min(delta.LengthSquared(), 100.0f) / 100.0f;
                    dir += delta.Normalized() * 30.0f * mod;
                }
            }
            

            Vector2 projected = Entity.Position + dir*0.01f*gameTime.ElapsedGameTime.Milliseconds;
            if (projected.X - rect.rect.Width/2.0f > Entity.World.Width)
            {
                projected.X = -rect.rect.Width/2.0f;
            }
            else if(projected.X + rect.rect.Width/2.0f < 0)
            {
                projected.X = Entity.World.Width + rect.rect.Width/2.0f;
            }

            if (projected.Y - rect.rect.Height/2.0f > Entity.World.Height)
            {
                projected.Y = -rect.rect.Height/2.0f;
            }
            else if (projected.Y + rect.rect.Height/2.0f < 0)
            {
                projected.Y = Entity.World.Height + rect.rect.Height/2.0f;
            }

            Entity.SetPosition(projected);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
         
        }

        public int BatchNo => 0;
    }
}
