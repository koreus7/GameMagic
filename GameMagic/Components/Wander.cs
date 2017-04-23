﻿using System;
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

        public Vector2 Dir {
            get { return dir;  }
        }
        
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

            float theta = Noise.Generate(x/300.0f + 2000.0f, y/300.0f + 2000.0f);
            dir = new Vector2(5.0f*(float)Math.Cos(theta*2*Math.PI),-5.0f*(float)Math.Sin(theta*2*Math.PI)) * 0.2f;

           // dir += new Vector2(x/w, y/h) * 0.1f;
           // dir += new Vector2((w - x)/w, (h - y)/y) * 0.1f;
            MouseState m = Mouse.GetState();

            float mouseDistance = Vector2.Distance(Entity.Position, new Vector2(m.X, m.Y));
            if (mouseDistance < 200 && m.RightButton == ButtonState.Pressed)
            {
                Vector2 delta = new Vector2(m.X - Entity.Position.X, m.Y - Entity.Position.Y);
                float mod = Math.Min(delta.LengthSquared(), 100.0f) / 100.0f;
                dir += delta.Normalized() * 35.0f * mod;
            }

            bool boost = false;

            foreach (RectColider.CollisionResult res in rect.Collisions)
            {

                RectColider colider = res.Collider;

                if (colider.Entity is Orb)
                {
                    Vector2 delta = (colider.Entity.Position - Entity.Position);
                    if (delta.LengthSquared() < 10)
                    {
                        // Atract when really close.
                        dir += delta.Normalized()*3.0f * MathHelper.Min(delta.LengthSquared(), 10.0f) / 10.0f;
                        //dir += delta.Normalized()*10.0f;
                    }
                    else if (delta.LengthSquared() < 200)
                    {
                        dir -= delta.Normalized()*1.0f;
                    }
                }
                else if (colider.Entity is Repelatron)
                {
                    Vector2 delta = (colider.Entity.Position - Entity.Position);
                    float nd = MathHelper.Min(300.0f, delta.LengthSquared())/ 300.0f;
                    float mod = 1.2f/MathHelper.Lerp(10f,0.2f,nd);
                    dir -= delta.Normalized()*mod;
                }
                else if (colider.Entity is Hub)
                {
                    Vector2 delta = colider.Entity.Position - Entity.Position;

                    if (delta.LengthSquared() < 80000.0f)
                    {
                        float mod = Math.Min(delta.LengthSquared(), 200.0f) / 200.0f;
                        dir += delta.Normalized() * 28.0f * mod;
                    }
                }
                else if (colider.Entity is HubCollider)
                {
                    if (res.JustEntered)
                    {
                        StaticSound.absorb.Play(0.1f, 1.0f, 1.0f);
                    }
                }
                else if (colider.Entity is Sink)
                {
                    Vector2 delta = colider.Entity.Position - Entity.Position;

                    if (delta.LengthSquared() < 80000.0f)
                    {
                        float mod = Math.Min(delta.LengthSquared(), 200.0f)/200.0f;
                        dir += delta.Normalized()*28.0f*mod;
                    }
                    if (delta.LengthSquared() < 10.0f)
                    {
                        Entity.GetComponent<SpriteRenderer>().Visible = false;
                    }

                }
                else if (colider.Entity is Planet)
                {
                    Vector2 normal = (colider.Entity.Position - Entity.Position).Normalized();
                    Vector2 tangent = new Vector2(-normal.Y, normal.X);

                    dir += tangent*10.0f;
                }
                else if (colider.Entity is ReversePlanet)
                {
                    Vector2 normal = (colider.Entity.Position - Entity.Position).Normalized();
                    Vector2 tangent = new Vector2(-normal.Y, normal.X);

                    dir -= tangent * 10.0f;
                }
                else if (colider.Entity is SpeedBoost)
                {
                    boost = true;
                }
            }

            if (boost)
            {
                dir *= 5.0f;
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
