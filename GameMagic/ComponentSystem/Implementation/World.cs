using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;
using GameMagic.Entities;
using GameMagic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenTK;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector4 = Microsoft.Xna.Framework.Vector4;

namespace GameMagic.ComponentSystem.Implementation
{
    public class World
    {
        private readonly ComponentStore components;
        private Dictionary<int, Entity> entities;
        private List<Entity> toBeAdded; 
        private MouseState lastMouse;
        private int entityIDCounter = 1;
        public int Width => Game.Width;
        public int Height => Game.Height;
        private bool updating = false;

        public CollisionSystem CollisionSystem { get; set; }
        public GMGame Game { get; private set; }

        public World(GMGame game)
        {
            Game = game;
            components = new ComponentStore();
            CollisionSystem = new CollisionSystem();
            entities = new Dictionary<int, Entity>();
            toBeAdded = new List<Entity>();
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
            entityIDCounter++;
            if (!updating)
            {
                InitAndAdd(e);
            }
            else
            {
                toBeAdded.Add(e);
            }
            return e;
        }

        private void InitAndAdd(Entity e)
        {
            e.Init();
            entities[e.ID] = e;
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            GMGame.lightEffect.Parameters["time"].SetValue(time.TotalGameTime.Milliseconds/1000.0f);
            GMGame.lightEffect.Parameters["col"].SetValue(new Vector4(1.0f,1.0f,1.0f,1.0f));
            batch.Begin(0, null, null, null, null, GMGame.lightEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 1))
            {
                c.Draw(time, batch);
            }
            batch.End();

            batch.Begin(0, null, null, null, null, GMGame.colliderEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 32))
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

            GMGame.lightEffect.Parameters["time"].SetValue(time.TotalGameTime.Milliseconds / 1000.0f);
            GMGame.lightEffect.Parameters["col"].SetValue(new Vector4(1.0f, 0.0f, 1.0f, 1.0f));
            batch.Begin(0, null, null, null, null, GMGame.lightEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 3))
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


            GMGame.sinkEffect.Parameters["res"].SetValue(new Vector2(1, 1));
            batch.Begin(0, null, null, null, null, GMGame.sinkEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 7))
            {
                c.Draw(time, batch);
            }
            batch.End();
        }


        public void AddEnt(Vector2 pos, int type)
        {
            switch (type)
            {
                case 0:
                    this.AddEntity(new Hub(this, pos));
                    break;
                case 1:
                    this.AddEntity(new Planet(this, pos));
                    break;
                case 2:
                    this.AddEntity(new Repelatron(this, pos));
                    break;
                case 3:
                    this.AddEntity(new Sink(this, pos));
                    break;
                case 4:
                    this.AddEntity(new Source(this, pos));
                    break;
            }
        }

        public int GetEntType(Entity e)
        {
            if (e is Hub)
            {
                return 0;
            }
            if (e is Planet)
            {
                return 1;
            }
            if (e is Repelatron)
            {
                return 2;
            }
            if (e is Sink)
            {
                return 3;
            }
            if (e is Source)
            {
                return 4;
            }

            return -1;
            
        }

        private class LevelData
        {
            public List<Vector2> Positions { get; set; } = new List<Vector2>();
            public List<int> EntityTypes { get; set; } = new List<int>();
        }

        public void Save()
        {
            LevelData data = new LevelData();
            foreach (KeyValuePair<int, Entity> keyValuePair in entities)
            {
                int type = GetEntType(keyValuePair.Value);

                if (type != -1)
                {
                    data.Positions.Add(keyValuePair.Value.Position);
                    data.EntityTypes.Add(type);
                }
            }

            string serialised = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            System.IO.File.WriteAllText(@"C:\Users\Public\Level.json", serialised);
        }

        public void Load()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\Public\Level.json");

            LevelData data = Newtonsoft.Json.JsonConvert.DeserializeObject<LevelData>(text);

            for (int i = 0; i < data.EntityTypes.Count; i++)
            {
                AddEnt(data.Positions[i], data.EntityTypes[i]);
            }

        }

        public void Update(GameTime time)
        {
            updating = true;
            MouseState ms = Mouse.GetState();
            
            CollisionSystem.Clear();
            CollisionSystem.Populate();
            foreach (IComponent c in components.GetComponents())
            {
                c.Update(time);
            }

            lastMouse = ms;
            updating = false;

            foreach (Entity entity in toBeAdded)
            {
                InitAndAdd(entity);
            }

            toBeAdded.Clear();
        }

        public virtual void Init()
        {

        }
    }
}
