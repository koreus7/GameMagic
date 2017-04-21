﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameMagic.ComponentSystem.Implementation
{
    public class Entity : IEntity
    {
        private readonly World _world;

        public Vector2 Position { get; set; }

        public Entity(World world, Vector2 pos)
        {
            _world = world;
            Position = pos;
        }

        public T GetComponent<T>() where T : IComponent
        {
            return _world.GetComponent<T>(this.ID);
        }

        public T AddNewComponent<T>() where T : IComponent, new()
        {
            var c = _world.NewComponent<T>(ID);
            c.Entity = this;
            c.Init();
            return c;
        }

        public virtual void Init()
        {

        }

        public int ID { get; set; }    
    }
}
