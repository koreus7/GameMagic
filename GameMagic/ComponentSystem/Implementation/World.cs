﻿using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.ComponentSystem.Implementation
{
    public class World
    {
        private readonly ComponentStore components;
        private Dictionary<int, Entity> entities;
        private int entityIDCounter = 1;

        public World()
        {
            components = new ComponentStore();
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
            foreach(IComponent c in components.GetComponents())
            {
                c.Draw(time, batch);
            }
        }

        public void Update(GameTime time)
        {
            foreach (IComponent c in components.GetComponents())
            {
                c.Update(time);
            }
        }

        public virtual void Init()
        {

        }
    }
}