using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;
using GameMagic.ComponentSystem;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;

namespace GameMagic.Entities
{
    class ForceNode : Entity
    {
        private Rectangle col;
        public ForceNode(World world, Vector2 pos, Rectangle col) : base(world, pos)
        {
            this.col = col;
        }

        public override void Init()
        {
            var r = this.AddNewComponent<RectColider>();
            r.rect = col;
            this.AddNewComponent<VectorNode>();
        }
    }
}
