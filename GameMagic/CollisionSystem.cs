using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;

namespace GameMagic
{
    public class CollisionSystem
    {
        private QuadTree<RectColider> quadTree;

        public CollisionSystem()
        {
            quadTree = new QuadTree<RectColider>(4,0,0,5000,5000);
        }

        public void AddRect(RectColider collider)
        {
            quadTree.Insert(collider, collider.WorldRect.X, collider.WorldRect.Y, collider.WorldRect.Width, collider.WorldRect.Height);
        }

        public void Clear()
        {
            quadTree.Clear();
        }
    }
}
