using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GameMagic.ComponentSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Components
{
    public class RectColider : IComponent, IDisposable
    {
        public int ID { get; set; }

        public IEntity Entity { get; set; }

        private Rectangle _rect = new Rectangle(0,0,0,0);

        public Rectangle rect {
            get { return _rect; }
            set { _rect = value; }
        }

        public Rectangle WorldRect
        {
            get {
                return new Rectangle(
                _rect.X + (int)Math.Floor(Entity.Position.X), 
                _rect.Y + (int)Math.Floor(Entity.Position.Y),
                _rect.Width,
                _rect.Height);  }
        }

        public bool WatchCollisions { get; set; } = false;

        public void Init()
        {
            Entity.World.CollisionSystem.Register(this);
        }


        private  List<RectColider> _collisions = new List<RectColider>();

        public List<RectColider> Collisions => _collisions;

        public void UpdateCollisions()
        {
            if (WatchCollisions)
            {
                Entity.World.CollisionSystem.GetCollisions(this, ref _collisions);
               // Logger.Log($"Rect Collider ID:{ID} Collisions: {_collisions.Count}");
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (GMGame.DebugOverlay)
            {
                float a = (float)Math.Min(Collisions.Count/5.0f, 1.0f);
                Entity.World.Game.DrawRectangle(WorldRect, Color.Red.MultiplyAlpha(a));
            }
        }

        public int BatchNo => 0;

        public void Dispose()
        {
            Entity.World.CollisionSystem.UnRegister(this);
        }
    }
}
