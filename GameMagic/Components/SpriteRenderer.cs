using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Components
{
    class SpriteRenderer : IComponent
    {
        public int ID { get; set; }

        public IEntity Entity { get; set; }

        public void Update(GameTime gameTime)
        {
            Logger.Log("Sprite Renderer " + ID + "update");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Logger.Log("Sprite Renderer " + ID + "draw");
        }
    }
}
