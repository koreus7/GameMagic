using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GameMagic.ComponentSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Components
{
    public class RectColider : IComponent
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


        public void Init()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            Entity.World.CollisionSystem.AddRect(this);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (GMGame.DebugOverlay)
            {
                Entity.World.Game.DrawRectangle(WorldRect, Color.Red.MultiplyAlpha(0.2f));
            }
        }
    }
}
