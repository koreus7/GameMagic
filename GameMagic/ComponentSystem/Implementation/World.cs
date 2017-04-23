using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;
using GameMagic.Entities;
using GameMagic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMagic.ComponentSystem.Implementation
{
    public class World
    {
        private readonly ComponentStore components;
        private Dictionary<int, Entity> entities;
        private MouseState lastMouse;
        private int entityIDCounter = 1;
        public int Width => Game.Width;
        public int Height => Game.Height;

        public CollisionSystem CollisionSystem { get; set; }
        public GMGame Game { get; private set; }

        public World(GMGame game)
        {
            Game = game;
            components = new ComponentStore();
            CollisionSystem = new CollisionSystem();
            entities = new Dictionary<int, Entity>();
        }

        public T GetComponent<T>(int entityId) where T : IComponent
        {
            return components.FindComponent<T>(entityId);
        }

        public T NewComponent<T>(int entityId) where T : IComponent, new()
        {
            return components.AddComponent<T>(entityId);
        }

        public T AddEntity<T>(T e) where T : Entity
        {
            e.ID = entityIDCounter;
            e.Init();
            entities[e.ID] = e;
            entityIDCounter++;
            return e;
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            GMGame.lightEffect.Parameters["time"].SetValue(time.TotalGameTime.Milliseconds/1000.0f);

            batch.Begin(0, null, null, null, null, GMGame.lightEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 1))
            {
                c.Draw(time, batch);
            }
            batch.End();

            batch.Begin();
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 0))
            {
                c.Draw(time, batch);
            }
            batch.End();


            GMGame.mouseEffect.Parameters["res"].SetValue(new Vector2(1, 1));
            batch.Begin(0, null, null, null, null, GMGame.mouseEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 2))
            {
                c.Draw(time, batch);
            }
            batch.End();


            GMGame.repelatronEffect.Parameters["res"].SetValue(new Vector2(1, 1));
            batch.Begin(0, null, null, null, null, GMGame.repelatronEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 5))
            {
                c.Draw(time, batch);
            }
            batch.End();

            GMGame.hubEffect.Parameters["res"].SetValue(new Vector2(1, 1));
            batch.Begin(0, null, null, null, null, GMGame.hubEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 6))
            {
                c.Draw(time, batch);
            }
            batch.End();
        }

        public void Update(GameTime time)
        {

            MouseState ms = Mouse.GetState();
            
            CollisionSystem.Clear();
            CollisionSystem.Populate();
            foreach (IComponent c in components.GetComponents())
            {
                c.Update(time);
            }

            if (ms.MiddleButton == ButtonState.Released && lastMouse.MiddleButton == ButtonState.Pressed)
            {
                this.AddEntity(new Hub(this, new Vector2(ms.X, ms.Y)));
            }


            if (ms.RightButton == ButtonState.Released && lastMouse.RightButton == ButtonState.Pressed)
            {
                this.AddEntity(new Repelatron(this, new Vector2(ms.X, ms.Y)));
            }

            lastMouse = ms;
        }

        public virtual void Init()
        {

        }
    }
}
